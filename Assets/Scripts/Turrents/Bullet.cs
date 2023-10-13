using UnityEngine;

public class Bullet : MonoBehaviour {
    [Header("Attributes")]
    [SerializeField] private float bulletSpeed;

    private Rigidbody2D rb;
    public Transform target { get; set; }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void RotateTowardsTarget() {
        transform.rotation = RotationHelpers.GetTargetAngle(transform, target.transform);
    }

    private void MoveTowardsTarget() {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }

    private void FixedUpdate() {
        if (target) {
            RotateTowardsTarget();
            MoveTowardsTarget();
        }
    }
}
