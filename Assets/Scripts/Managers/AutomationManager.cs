using System;
using System.Collections.Concurrent;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace Managers
{
    // Have this class manage automation. Fighting automation for starters
    // Other automation possibilities:
    // Poison ticks
    // Health regeneration
    public class AutomationManager : MonoBehaviour
    {
        [NonSerialized] 
        public static AutomationManager Instance;

        public float timeScale = 1f;

        public float TimeDelta => Time.deltaTime;
        
        private ConcurrentDictionary<GUID, TimingThing> _registrations = new ();

        private bool _updateRunning = false;

        private bool _canRunUpdate = true;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Update()
        {
            UpdateRegistrations();
        }

        private void UpdateRegistrations()
        {
            if (!_canRunUpdate) return;
            
            _updateRunning = true;

            foreach (var (key, timingThing) in _registrations)
            {
                if (!timingThing.IsActive) continue;
                
                if (timingThing.WaitTimeRemaining <= 0)
                {
                    timingThing.Run();
                    timingThing.Reset();
                }

                timingThing.TimeValueRemaining -= TimeDelta;
            }
            
            _updateRunning = false;
        }

        public TimingThing Register(float interval, Action action)
        {
            var result = new TimingThing(action)
            {
                Interval = interval,
                TimeValueRemaining = interval,
                TimeScale = timeScale,
            };

            _registrations.TryAdd(result.Id, result);

            return result;
        }
        
        public class TimingThing
        {
            private readonly Action _action;
            public readonly GUID Id;
            
            public bool CanActivate => TimeValueRemaining <= 0;
            public float WaitTimeRemaining => TimeValueRemaining * TimeScale; 
            
            public float TimeValueRemaining;
            public float Interval;
            public float TimeScale;

            private bool _isActive = true;
            public bool IsActive => _isActive; 

            public TimingThing(Action action)
            {
                _action = action;
                Id = GUID.Generate();
            }

            public void Disable()
            {
                _isActive = false;
            }
            
            public void Run()
            {
                _action.Invoke();
            }

            public void Reset()
            {
                TimeValueRemaining = Interval;
            }
        }
    }
}