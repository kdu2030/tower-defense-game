using UnityEngine;

public class TurrentCannon : MonoBehaviour {
    [Header("References")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform rotationPoint;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Transform bulletsParent;
    [SerializeField] private GameObject bulletPrefab;

    [Header("References")]
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bulletsPerSecond = 5f;

    private Transform target;
    private CircleCollider2D circleCollider;
    private float timeSinceFired = 0f;

    private void Start() {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private bool IsTargetInRange() {
        return Vector2.Distance(target.position, transform.position) <= circleCollider.radius;
    }

    private void RotateTowardsTarget() {
        Quaternion targetAngle = RotationHelpers.GetTargetAngle(transform, target);
        rotationPoint.rotation = Quaternion.RotateTowards(transform.rotation, targetAngle, rotationSpeed * Time.deltaTime);
    }

    private void FindTarget() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius, enemyLayer);
        if (colliders.Length > 0) {
            target = colliders[0].gameObject.transform;
        }
    }

    private void Shoot() {
        GameObject bulletObject = Instantiate(bulletPrefab, spawnPoint.position, rotationPoint.rotation, bulletsParent);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.target = target;
    }

    private void HandleShoot() {
        timeSinceFired += Time.deltaTime;

        if (timeSinceFired >= 1f / bulletsPerSecond) {
            Shoot();
            timeSinceFired = 0f;
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
            HandleShoot();
        }

    }
}
