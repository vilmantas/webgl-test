using System;
using System.Collections;
using System.Collections.Generic;
using Modules;
using UnityEngine;

public class HealthDisplayScript : MonoBehaviour
{
    [NonSerialized]
    public Fighter Fighter;

    private static int width = 100;
    private static int height = 20;
    Rect rect = new Rect(0, 0, width, height);
    Vector3 offset = new Vector3(0f, 0f, 0.5f); // height above the target position
    
    private void OnGUI()
    {
        if (Fighter == null) return;
        Vector3 point = Camera.main.WorldToScreenPoint(transform.position + offset);
        rect.x = point.x - (width / 2);
        rect.y = Screen.height - point.y - rect.height;
        GUI.Box(rect, $"Power: {Fighter.Power}");
        rect.y -= height;
        GUI.Box(rect, $"Health: {Fighter.HealthValue}/{Fighter.MaxHealth}");

    }
}
