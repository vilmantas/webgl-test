using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SimpleCubeScriptTest
{
    [UnityTest]
    public IEnumerator SimpleCubeScriptTestWithEnumeratorPasses()
    {
        var gameObject = new GameObject();
        var sut = gameObject.AddComponent<SimpleCubeScript>();

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        
        Assert.AreEqual(true, sut.IsActive);
    }
}
