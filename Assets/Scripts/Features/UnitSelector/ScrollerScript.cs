using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;

public class ScrollerScript : MonoBehaviour
{
    public static FighterScriptable SelectedFighter;

    public GameObject spawn;
    
    public FighterScriptable[] selectables;

    public Camera selectionCamera;

    private GameObject _mCurrentItemInstance;

    private int _mCurrentIndex;

    private Camera _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        var selection = ScriptableObject.FindObjectOfType<FightersCollection>();

        Debug.Log(selection == null);

        SelectedFighter = selectables.First();
        _mCurrentIndex = 0;
        _mainCamera = Camera.main;
        selectionCamera.enabled = true;

        if (Camera.main == null) return;
        Camera.main.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_mCurrentItemInstance == null) _mCurrentItemInstance = Instantiate<GameObject>(SelectedFighter.Model, spawn.transform);

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Destroy(_mCurrentItemInstance);
            _mCurrentIndex = (_mCurrentIndex + 1) % selectables.Length;
            SelectedFighter = selectables[_mCurrentIndex];
            _mCurrentItemInstance = Instantiate<GameObject>(SelectedFighter.Model, spawn.transform);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Destroy(_mCurrentItemInstance);
            _mCurrentIndex = ((_mCurrentIndex - 1) + selectables.Length) % selectables.Length;
            SelectedFighter = selectables[_mCurrentIndex];
            _mCurrentItemInstance = Instantiate<GameObject>(SelectedFighter.Model, spawn.transform); 
        }
    }
}
