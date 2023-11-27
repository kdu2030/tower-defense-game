using System;
using UnityEngine;

[Serializable]
public class PlaceableObject {
    // [field: SerializeField] allows this field to be displayed in the inspector.
    // {get; private set;} in this context means that the value can only be set in the Inspector
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public int ID { get; private set; }
    [field: SerializeField]
    public Vector2Int Size { get; set; } = Vector2Int.one;
    [field: SerializeField]
    public GameObject Prefab { get; private set; }

    [field: SerializeField]
    public bool OverrideCalculatedSize { get; private set; }


}