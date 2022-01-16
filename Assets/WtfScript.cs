using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WtfScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var z = gameObject.GetComponentsInParent<TextMeshProUGUI>();
    }
}
