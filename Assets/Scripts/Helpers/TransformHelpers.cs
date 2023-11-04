using UnityEngine;

public class TransformHelpers {
    public static Quaternion GetTargetAngle(Transform baseTransform, Transform targetTransform) {
        float targetAngleDeg = Mathf.Atan2(targetTransform.position.y - baseTransform.position.y, targetTransform.position.x - baseTransform.position.x) * Mathf.Rad2Deg - 90f;
        return Quaternion.Euler(0, 0, targetAngleDeg);
    }

    public static Vector2 GetScaleFromDimensions(Vector2 originalDimensions, Vector2 targetDimensions) {
        return targetDimensions / originalDimensions;
    }

}
