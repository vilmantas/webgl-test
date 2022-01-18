using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectableItem
{
    GameObject Prefab { get; set; }

    string Definition { get; set; }
}
