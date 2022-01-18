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
        private static AutomationManager _instance;

        public float timeScale = 1f;

        private float TimeDelta => Time.deltaTime;
        
        private ConcurrentDictionary<GUID, TimingThing> _registrations = new ();

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        private void Update()
        {
            UpdateRegistrations();
        }

        private void UpdateRegistrations()
        {
            foreach (var (key, timingThing) in _registrations)
            {
                timingThing.TimeValueRemaining -= TimeDelta;

                if (timingThing.WaitTimeRemaining <= 0)
                {
                    timingThing.Run();
                    timingThing.Reset();
                }
            }
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

            public bool IsActive = true;

            public TimingThing(Action action)
            {
                _action = action;
                Id = GUID.Generate();
            }

            public void Unregister()
            {
                IsActive = false;
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