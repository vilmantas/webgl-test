using System;
using UnityEngine;
using Managers;

public class Preloader
{
    // Runs before a scene gets loaded
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadMain()
    {
        var boss = Resources.Load<GameObject>("Managers") as GameObject;
        var man = boss.GetComponentInChildren<GlobalsManager>();
        man.DoSetup();
        GameObject.DontDestroyOnLoad(boss);
    }
}
