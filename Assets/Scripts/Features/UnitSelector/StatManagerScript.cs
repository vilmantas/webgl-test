using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManagerScript : MonoBehaviour
{
    StatScript Health;
    StatScript Power;
    StatScript AttackRate;

    // Start is called before the first frame update
    void Start()
    {
        Health = FindStat("Health");
        Power = FindStat("Power");
        AttackRate = FindStat("AttackRate");
    }

    // Update is called once per frame
    void Update()
    {
        Power.Value = ScrollerScript.SelectedFighter.Strength.ToString();
        Health.Value = ScrollerScript.SelectedFighter.Health.ToString();
        AttackRate.Value = (1 / ScrollerScript.SelectedFighter.DelayBetweenAttacks).ToString("0.0");

    }

    private StatScript FindStat(string name)
    {
        return transform.Find(name + "Stat").GetComponentInChildren<StatScript>();
    }
}
