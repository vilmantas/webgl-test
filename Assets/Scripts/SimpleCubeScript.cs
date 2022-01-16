using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCubeScript : MonoBehaviour
{
    public bool IsActive = false;
    
    // Start is called before the first frame update
    void Start()
    {
        IsActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.up, 10 * Time.deltaTime);
    }
}
