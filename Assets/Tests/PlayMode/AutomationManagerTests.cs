using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Managers;

public class AutomationManagerTests
{
    private AutomationManager _sut;

    private float _testStartTime;

    private float _timeSinceTestStart => Time.timeSinceLevelLoad - _testStartTime;
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var o = new GameObject();
        var component = o.AddComponent<AutomationManager>();
    }

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        _testStartTime = Time.timeSinceLevelLoad;

        yield return null;
    }
    
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator AutomationManagerTestsWithEnumeratorPasses()
    {
        Assert.IsTrue(true);
        yield return null;
    }

    [UnityTest]
    public IEnumerator AutomationManager_CreatesTimingWithInterval()
    {
        var result = AutomationManager.Instance.Register(1f, () => { return; });
        
        Assert.AreEqual(1f, result.Interval);

        yield return null;
    }

    [UnityTest]
    public IEnumerator AutomationManager_UpdatesTimingAfterFrame()
    {
        yield return new WaitForEndOfFrame();

        var result = AutomationManager.Instance.Register(1f, () => { return; });

        yield return new WaitForEndOfFrame();

        Assert.AreEqual(1f - AutomationManager.Instance.TimeDelta, result.WaitTimeRemaining);

        yield return null;
    }

    [UnityTest]
    public IEnumerator AutomationManager_FiresActionAfterTimeElapsed()
    {
        var run_tracker = false;
        var result = AutomationManager.Instance.Register(1f, () => { run_tracker = true; });

        yield return new WaitUntil(() => run_tracker || _timeSinceTestStart > 2f);
        
        Assert.IsTrue(run_tracker);
        Assert.IsFalse(result.CanActivate);

        yield return null;
    }

    [UnityTest]
    public IEnumerator AutomationManager_OnlyRunsCodeOnce()
    {
        var counter = 0;
        var result = AutomationManager.Instance.Register(1f, () => { counter = counter + 1; });

        yield return new WaitUntil(() => counter > 0 || _timeSinceTestStart > 2f);
        
        Assert.AreEqual(1, counter);
        Assert.IsFalse(result.CanActivate);

        yield return null;
    }

    [UnityTest]
    public IEnumerator AutomationManager_UpdatesMultipleRegistrations()
    {
        yield return new WaitForEndOfFrame();

        var registration1 = AutomationManager.Instance.Register(1f, () => { return; });
        var registration2 = AutomationManager.Instance.Register(1f, () => { return; });

        yield return new WaitForEndOfFrame();
        
        Assert.AreEqual(1f - AutomationManager.Instance.TimeDelta, registration1.TimeValueRemaining);
        Assert.AreEqual(1f - AutomationManager.Instance.TimeDelta, registration2.TimeValueRemaining);
    }

    [UnityTest]
    public IEnumerator AutomationManager_DoesNotRunDisabledTimers()
    {
        yield return new WaitForEndOfFrame();

        var ran_first_registration = false;
        var ran_second_registration = false;
        
        var registration1 = AutomationManager.Instance.Register(1f, () => { ran_first_registration = true; });
        var registration2 = AutomationManager.Instance.Register(1f, () => { ran_second_registration = true; });
        
        registration2.Disable();
        
        yield return new WaitUntil(() => _timeSinceTestStart > 2f);
 
        Assert.IsTrue(ran_first_registration);
        Assert.IsFalse(ran_second_registration);
    }

    [UnityTest]
    public IEnumerator AutomationManager_CallsTimerMultipleTimes()
    {
        yield return new WaitForEndOfFrame();

        var run_counter = 0;
        
        var registration = AutomationManager.Instance.Register(1f, () => { run_counter += 1; });

        yield return new WaitUntil(() => _timeSinceTestStart > 4f || run_counter == 2);
        
        Assert.AreEqual(2, run_counter);
    }
}
