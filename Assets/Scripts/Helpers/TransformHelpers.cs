using Unity.VisualScripting;
using UnityEngine;

public class TransformHelpers {
    public static Quaternion GetTargetAngle(Transform baseTransform, Transform targetTransform) {
        float targetAngleDeg = Mathf.Atan2(targetTransform.position.y - baseTransform.position.y, targetTransform.position.x - baseTransform.position.x) * Mathf.Rad2Deg - 90f;
        return Quaternion.Euler(0, 0, targetAngleDeg);
    }

    public static Vector2 GetScaleFromDimensions(Vector2 originalDimensions, Vector2 targetDimensions) {
        return targetDimensions / originalDimensions;
    }

    public static Vector2Int GetSpriteSizeInGridCells(SpriteRenderer gameObjectSprite, Vector2Int gridCellSize) {
        int sizeXInGridUnits = Mathf.CeilToInt(gameObjectSprite.bounds.size.x / gridCellSize.x);
        int sizeYInGridUnits = Mathf.CeilToInt(gameObjectSprite.bounds.size.y / gridCellSize.y);

        return new Vector2Int(sizeXInGridUnits, sizeYInGridUnits);
    }

}
