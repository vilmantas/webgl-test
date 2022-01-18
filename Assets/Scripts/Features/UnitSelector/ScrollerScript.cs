using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrollerScript : MonoBehaviour
{
    public GameObject spawn;
    
    public GameObject[] selectables;

    public Camera selectionCamera;
    
    private GameObject _mCurrentItem;

    private GameObject _mCurrentItemInstance;

    private int _mCurrentIndex;

    private Camera _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _mCurrentItem = selectables.First();
        _mCurrentIndex = 0;
        _mainCamera = Camera.main;
        selectionCamera.enabled = true;

        if (Camera.main == null) return;
        Camera.main.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_mCurrentItemInstance == null) _mCurrentItemInstance = Instantiate<GameObject>(_mCurrentItem, spawn.transform);

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Destroy(_mCurrentItemInstance);
            _mCurrentIndex = (_mCurrentIndex + 1) % selectables.Length;
            _mCurrentItem = selectables[_mCurrentIndex];
            _mCurrentItemInstance = Instantiate<GameObject>(_mCurrentItem, spawn.transform);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Destroy(_mCurrentItemInstance);
            _mCurrentIndex = ((_mCurrentIndex - 1) + selectables.Length) % selectables.Length;
            _mCurrentItem = selectables[_mCurrentIndex];
            _mCurrentItemInstance = Instantiate<GameObject>(_mCurrentItem, spawn.transform); 
        }
    }
}
