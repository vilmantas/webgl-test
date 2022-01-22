using System;
using UnityEditor;
using UnityEngine;

public class TimingRegistration
{
    public readonly string Name;
    private readonly Action _action;
    public readonly GUID Id;

    private float _timeValueRemaining;
    public float Interval;
    public float TimeScale;
    public bool Autorun = true;
    public bool AllowAutorunToggle = true;
    public bool AutoReset = true;

    private bool _isActive = true;
    public bool IsActive => _isActive;
    public bool CanActivate => _timeValueRemaining <= 0;
    public float WaitTimeRemaining => _timeValueRemaining * TimeScale;
    public float IntervalScaled => Interval * TimeScale;
    public float WaitTimeRemainingPercentage => WaitTimeRemaining / IntervalScaled;
    public float TimeValueRemaining => _timeValueRemaining;

    public TimingRegistration(Action action, string name, float interval)
    {
        _action = action;
        _timeValueRemaining = interval;

        Id = GUID.Generate();
        Name = name;
        Interval = interval;
    }

    public void Disable()
    {
        _isActive = false;
    }

    public void Run()
    {
        _action.Invoke();
        if (AutoReset) Reset();
    }

    private void Reset()
    {
        _timeValueRemaining = Interval;
    }

    public void ToggleAutorun()
    {
        if (!AllowAutorunToggle) return;
        Autorun = !Autorun;
    }

    public void UpdateRemainingTime(float delta)
    {
        var newValue = _timeValueRemaining - delta;
        _timeValueRemaining = Mathf.Max(0, newValue);

        TryRun();
    }

    private void TryRun()
    {
        if (_timeValueRemaining == 0 && Autorun)
        {
            Run();
        }
    }
}