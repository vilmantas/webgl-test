using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownComponentScript : MonoBehaviour
{
    public TextMeshProUGUI NameField;
    public TextMeshProUGUI DurationLeftField;
    public Image OutlineField;


    [NonSerialized] public string Name = "";
    [NonSerialized] public float DurationLeft = 0f;
    [NonSerialized] public float DurationLeftPercentage = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        NameField.text = Name;
    }

    // Update is called once per frame
    void Update()
    {
        DurationLeftField.text = DurationLeft.ToString("0.0");
        OutlineField.transform.localScale = new Vector3(DurationLeftPercentage, 1, 1);
    }
}
