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
        Vector2 direction = (target.position - transform.position).normalized;
        float targetBulletAngle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion bulletRotationTarget = Quaternion.Euler(0, 0, targetBulletAngle);
        transform.rotation = bulletRotationTarget;
        rb.velocity = direction * bulletSpeed;
    }

    private void FixedUpdate() {
        if (target) {
            RotateTowardsTarget();
        }
    }
}
