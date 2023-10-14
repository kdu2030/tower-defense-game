using UnityEngine;

public class Bullet : MonoBehaviour {
    [Header("References")]
    [SerializeField] private LayerMask enemyLayer;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed;

    private Rigidbody2D rb;
    private int enemyLayerNum;
    public Transform target { get; set; }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        enemyLayerNum = Mathf.RoundToInt(Mathf.Log(enemyLayer.value, 2f));
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

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == enemyLayerNum) {
            Destroy(gameObject);
        }
    }
}
