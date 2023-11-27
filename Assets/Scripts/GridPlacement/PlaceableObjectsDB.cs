using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlaceableObjectsDB : ScriptableObject {
    [SerializeField]
    private Vector2Int gridCellSize;

    [SerializeField]
    private List<PlaceableObject> placeableObjects;

    private void OnValidate() {
        if (gridCellSize.x <= 0 || gridCellSize.y <= 0) {
            return;
        }

        foreach (PlaceableObject placeableObject in placeableObjects) {
            SpriteRenderer towerGameObject = placeableObject?.Prefab?.GetComponentInChildren<SpriteRenderer>();
            if (placeableObject.OverrideCalculatedSize || towerGameObject == null) {
                continue;
            }
            int sizeXInGridUnits = Mathf.CeilToInt(towerGameObject.bounds.size.x / gridCellSize.x);
            int sizeYInGridUnits = Mathf.CeilToInt(towerGameObject.bounds.size.y / gridCellSize.y);
            placeableObject.Size = new Vector2Int(sizeXInGridUnits, sizeYInGridUnits);
        }
    }

}
