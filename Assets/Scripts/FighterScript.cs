using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Managers;
using Modules;
using Modules.Commands;
using ScriptableObjects;
using UnityEngine;

public class FighterScript : MonoBehaviour
{
    public ParticleSystem DamageParticles;
    
    public HealthScript HealthScript;
    
    public FighterScriptable fighterScriptable;

    private Fighter _fighter;

    public bool IsDead => _fighter.IsDead;

    private float _timeSinceAttack = 0f;

    private readonly ConcurrentQueue<FighterCommand> _queue = new ();

    // Start is called before the first frame update
    private void Start()
    {
        _fighter = fighterScriptable.Build();
        if (HealthScript == null) return;
        HealthScript.Fighter = _fighter;
        IEnumerator coroutine = ProcessCommands();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_fighter.Health <= 0)
        {
            Debug.Log($"---- {tag} --- Is Dead!");
            Destroy(gameObject);
        }
        
        _timeSinceAttack += Time.deltaTime;
        
        if (!CanAttack) return;
        
        CombatManager.Instance.HandleAttack(this);

        _timeSinceAttack = 0f;
    }

    private bool CanAttack => _timeSinceAttack > _fighter.AttackSpeed;

    public void DefendFrom(FighterScript attacker)
    {
        _queue.Enqueue(new FighterCommand(DefendFromAction(attacker)));
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
