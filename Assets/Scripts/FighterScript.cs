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

    private readonly ConcurrentQueue<FighterCommand> _queue = new ();

    private AutomationManager.TimingThing _autoAttackRegistration;

    private AutomationManager.TimingThing _healthRegenRegistration;
    // Start is called before the first frame update
    private void Start()
    {
        _fighter = fighterScriptable.Build();
        
        _autoAttackRegistration = AutomationManager.Instance.Register(_fighter.AttackSpeed,
            () => CombatManager.Instance.HandleAttack(this), "Attack", allowToggle: false);

        _healthRegenRegistration =
            AutomationManager.Instance.Register(Random.Range(5,8), () => GainHealth(1), "Regeneration", allowToggle: false);
        
        if (healthDisplayScript == null) return;
        
        healthDisplayScript.Fighter = _fighter;
        timingDisplayScript.Timings = new List<AutomationManager.TimingThing>() { _autoAttackRegistration, _healthRegenRegistration };
        
        IEnumerator coroutine = ProcessCommands();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_fighter.HealthValue > 0) return;
        
        Debug.Log($"---- {tag} --- Is Dead!");
        _autoAttackRegistration.Disable();
        Destroy(gameObject);
    }

    public void DefendFrom(FighterScript attacker)
    {
        _queue.Enqueue(new FighterCommand(DefendFromAction(attacker)));
    }

    public void GainHealth(float amount)
    {
        _queue.Enqueue(new FighterCommand((GainHealthAction(this))));
    }

    private static Action<FighterScript> DefendFromAction(FighterScript attacker)
    {
        return ctx =>
        {
            ctx._fighter.Defend(attacker._fighter);
            if (ctx._fighter.IsDead) return;
            ctx.DamageParticles.Play();
        };
    }
    
    private static Action<FighterScript> GainHealthAction(FighterScript wtf)
    {
        return ctx =>
        {
            ctx._fighter.Health.Gain(1);
        };
    }
    
    private IEnumerator ProcessCommands()
    {
        FighterCommand command;
        while (true)
        {
            if (_queue.TryDequeue(out command))
            {
                command.Run(this);
            }

            yield return null;
        }
    }
}
