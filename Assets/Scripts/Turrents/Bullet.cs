using UnityEngine;

public class Bullet : MonoBehaviour {
    [Header("References")]
    [SerializeField] private LayerMask enemyLayer;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int damage = 1;

    private Rigidbody2D rb;
    private int enemyLayerNum;
    public GameObject target { get; set; }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        enemyLayerNum = Mathf.RoundToInt(Mathf.Log(enemyLayer.value, 2f));
    }

    private void RotateTowardsTarget() {
        transform.rotation = RotationHelpers.GetTargetAngle(transform, target.transform);
    }

    private void MoveTowardsTarget() {
        Vector2 direction = (target.transform.position - transform.transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }

    private bool IsStillAlive() {
        EnemyLife enemyLife = target.GetComponent<EnemyLife>();
        return enemyLife.LivesRemaining > 0;
    }

    private void FixedUpdate() {
        if (target && !IsStillAlive()) {
            Destroy(gameObject);
        }

        if (target) {
            RotateTowardsTarget();
            MoveTowardsTarget();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject targetGameObject = collision.gameObject;

        if (targetGameObject.layer == enemyLayerNum) {
            EnemyLife enemyLife = targetGameObject.GetComponent<EnemyLife>();
            enemyLife.UpdateLivesRemaining(damage);
            Destroy(gameObject);
        }
    }
}
