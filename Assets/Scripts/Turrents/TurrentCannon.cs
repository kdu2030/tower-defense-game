using UnityEngine;

public class TurrentCannon : MonoBehaviour {
    [Header("References")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform rotationPoint;

    [Header("References")]
    [SerializeField] private float rotationSpeed = 5f;

    private Transform target;
    private CircleCollider2D circleCollider;

    private void Start() {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private bool IsTargetInRange() {
        return Vector2.Distance(target.position, transform.position) <= circleCollider.radius;
    }

    private void RotateTowardsTarget() {
        float rotationAngleDeg = Mathf.Atan2(transform.position.y - target.position.y, transform.position.x - target.position.x) * Mathf.Rad2Deg + 90f;
        Quaternion targetAngle = Quaternion.Euler(0, 0, rotationAngleDeg);
        rotationPoint.rotation = Quaternion.RotateTowards(transform.rotation, targetAngle, rotationSpeed * Time.deltaTime);
    }

    private void FindTarget() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius, enemyLayer);
        if (colliders.Length > 0) {
            target = colliders[0].gameObject.transform;
        }
    }

    private void FixedUpdate() {
        if (target == null) {
            FindTarget();
        }

        if (target != null && !IsTargetInRange()) {
            target = null;
        }
        else if (target != null) {
            RotateTowardsTarget();
        }

    }
}
