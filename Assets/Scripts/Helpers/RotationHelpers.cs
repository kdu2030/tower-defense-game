using UnityEngine;

public class RotationHelpers {
    public static Quaternion GetTargetAngle(Transform baseTransform, Transform targetTransform) {
        float targetAngleDeg = Mathf.Atan2(targetTransform.position.y - baseTransform.position.y, targetTransform.position.x - baseTransform.position.x) * Mathf.Rad2Deg - 90f;
        return Quaternion.Euler(0, 0, targetAngleDeg);
    }

}
