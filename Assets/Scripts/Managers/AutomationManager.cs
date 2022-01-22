using System;
using System.Collections;
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
        
        private readonly ConcurrentDictionary<GUID, TimingRegistration> _registrations = new ();

        private readonly ConcurrentQueue<TimingRegistration> _activatableTimers = new ();

        private bool _updateRunning = false;

        private static bool _canRunUpdate => !GameManager.IsGameOver;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            IEnumerator coroutine = ProcessCommands();
            StartCoroutine(coroutine);
        }

        private void Update()
        {
            UpdateRegistrations();
        }

        private void UpdateRegistrations()
        {
            if (!_canRunUpdate) return;
            
            _updateRunning = true;

            UpdateTimings();

            RemoveInactiveTimings();

            _updateRunning = false;
        }

        private void UpdateTimings()
        {
            var activeRegistrations = _registrations.Where(x => x.Value.IsActive);

            foreach (var (key, timingThing) in activeRegistrations)
            {
                UpdateTiming(timingThing);
            }
        }

        private void RemoveInactiveTimings()
        {
            var toRemove = _registrations.Where(x => !x.Value.IsActive).Select(x => x.Key);

            foreach (var key in toRemove)
            {
                _registrations.TryRemove(key, out var _);
            }
        }

        private void UpdateTiming(TimingRegistration timingRegistration)
        {
            timingRegistration.UpdateRemainingTime(TimeDelta);
        }

        public TimingRegistration Register(float interval, Action action, string name = "", bool autoRun = true, bool allowToggle = true)
        {
            var result = new TimingRegistration(action, name, interval)
            {
                TimeScale = timeScale,
                Autorun = autoRun,
                AllowAutorunToggle = allowToggle,
            };

            _registrations.TryAdd(result.Id, result);

            return result;
        }
        
        private IEnumerator ProcessCommands()
        {
            while (true)
            {
                if (_activatableTimers.TryDequeue(out var timer))
                {
                    timer.Run();
                }

                yield return null;
            }
        }
    }
}