using System;
using System.Collections.Concurrent;
using System.Linq;
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

        private bool _canRunUpdate => !GameManager.Instance.IsGameOver;
        
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

            foreach (var (key, timingThing) in _registrations.Where(x => x.Value.IsActive))
            {
                UpdateTiming(timingThing);
            }
            
            _updateRunning = false;
        }

        private void UpdateTiming(TimingThing timingThing)
        {
            if (timingThing.WaitTimeRemaining <= 0 && timingThing.Autorun)
            {
                timingThing.Run();
                timingThing.Reset();
            }

            var newValue = timingThing.TimeValueRemaining - TimeDelta;
            timingThing.TimeValueRemaining = Mathf.Max(0, newValue);
        }

        public TimingThing Register(float interval, Action action, string name = "", bool autoRun = true, bool allowToggle = true)
        {
            var result = new TimingThing(action, name)
            {
                Interval = interval,
                TimeValueRemaining = interval,
                TimeScale = timeScale,
                Autorun = autoRun,
                AllowAutorunToggle = allowToggle
            };

            _registrations.TryAdd(result.Id, result);

            return result;
        }
        
        public class TimingThing
        {
            public readonly string Name;
            private readonly Action _action;
            public readonly GUID Id;

            public float TimeValueRemaining;
            public float Interval;
            public float TimeScale;
            public bool Autorun = true;
            public bool AllowAutorunToggle = true;

            private bool _isActive = true;
            public bool IsActive => _isActive; 
            public bool CanActivate => TimeValueRemaining <= 0;
            public float WaitTimeRemaining => TimeValueRemaining * TimeScale;
            public float IntervalScaled => Interval * TimeScale;
            public float WaitTimeRemainingPercentage => WaitTimeRemaining / IntervalScaled;

            public TimingThing(Action action, string name)
            {
                _action = action;
                Id = GUID.Generate();
                Name = name;
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

            public void ToggleAutorun()
            {
                if (!AllowAutorunToggle) return;
                Autorun = !Autorun;
            }
        }
    }
}