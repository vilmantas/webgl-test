using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DefaultNamespace;
using Managers;
using Modules;
using Modules.Commands;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class FighterScript : MonoBehaviour
{
    public ParticleSystem DamageParticles;
    
    public HealthDisplayScript healthDisplayScript;

    public TimingsDisplayScript timingDisplayScript;
    
    public FighterScriptable fighterScriptable;

    private Fighter _fighter;

    public bool IsDead => _fighter.IsDead;

    private readonly ConcurrentQueue<Action> _queue = new();

    private readonly List<TimingRegistration> _timings = new();

    // Start is called before the first frame update
    private void Start()
    {
        if (fighterScriptable == null) return;
        
        _fighter = fighterScriptable.Build();

        AddTimers();
        
        healthDisplayScript.Fighter = _fighter;
        timingDisplayScript.Timings = _timings;
        
        IEnumerator coroutine = ProcessCommands();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_fighter == null) return;
        if (_fighter.HealthValue > 0) return;
        
        DoDeath();
    }

    public void DefendFrom(FighterScript attacker)
    {
        _queue.Enqueue(DefendFromAction(attacker));
    }

    public void GainHealth(float amount)
    {
        _queue.Enqueue((GainHealthAction(amount)));
    }

    private Action DefendFromAction(FighterScript attacker)
    {
        return () =>
        {
            _fighter.Defend(attacker._fighter);
            if (_fighter.IsDead) return;
            DamageParticles.Play();
        };
    }
    
    private Action GainHealthAction(float amount)
    {
        return () =>
        {
            _fighter.Health.Gain(amount);
        };
    }
    
    private IEnumerator ProcessCommands()
    {
        while (true)
        {
            if (_queue.TryDequeue(out var command))
            {
                command.Invoke();
            }

            yield return null;
        }
    }

    private void AddTimers()
    {
        var autoAttackRegistration = AutomationManager.Instance.Register(_fighter.AttackSpeed,
            () => CombatManager.Instance.HandleAttack(this), "Attack", allowToggle: false);

        var healthRegenRegistration =
            AutomationManager.Instance.Register(Random.Range(5,8), () => GainHealth(1), "Regeneration", allowToggle: false);
        
        _timings.Add(autoAttackRegistration);
        _timings.Add(healthRegenRegistration);
    }

    private void DoDeath()
    {
        Debug.Log($"---- {_fighter.Name} --- Is Dead!");
        ClearTimings();
        Destroy(gameObject);
    }

    private void ClearTimings()
    {
        _timings.ForEach(x => x.Disable());
        _timings.Clear();
    }
}
