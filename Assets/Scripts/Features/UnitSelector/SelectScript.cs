using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectScript : MonoBehaviour
{
    private Button Button;

    // Start is called before the first frame update
    void Start()
    {
        Button = transform.Find("Select").GetComponent<Button>();
        Button.onClick.AddListener(() => Debug.Log("Clicked"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
