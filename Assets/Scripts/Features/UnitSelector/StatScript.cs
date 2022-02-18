using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatScript : MonoBehaviour
{
    [HideInInspector]
    public string Value;

    private TextMeshProUGUI TitleField;
    private TextMeshProUGUI ValueField;

    // Start is called before the first frame update
    void Start()
    {
        TitleField = transform.Find("Title").gameObject.GetComponent<TextMeshProUGUI>();
        ValueField = transform.Find("Value").gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        ValueField.text = Value;
    }
}
