using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlaceableObjectsDB : ScriptableObject {
    [SerializeField]
    private Vector2Int gridCellSize;

    [field: SerializeField]
    public List<PlaceableObject> PlaceableObjects { get; set; }

    private void OnValidate() {
        if (gridCellSize.x <= 0 || gridCellSize.y <= 0) {
            return;
        }

        foreach (PlaceableObject placeableObject in PlaceableObjects) {
            if (placeableObject.OverrideCalculatedSize || placeableObject?.Prefab == null) {
                continue;
            }

            SpriteRenderer towerGameObject = placeableObject.Prefab.GetComponentInChildren<SpriteRenderer>();
            if (towerGameObject == null) {
                continue;
            }
            placeableObject.Size = TransformHelpers.GetSpriteSizeInGridCells(towerGameObject, gridCellSize);
        }
    }

}
