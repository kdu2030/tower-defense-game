using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [SerializeField]
    private float enemySpeed;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private EnemyLife enemyLife;

    private int directionHash;
    private int actionHash;
    private int waypointIndex = 0;

    private void Start() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyLife = GetComponent<EnemyLife>();
        directionHash = Animator.StringToHash("Direction");
        actionHash = Animator.StringToHash("Action");
    }

    private Vector3 MoveEnemy() {
        Transform targetWaypoint = EnemyWaypoints.waypoints[waypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;
        transform.Translate(direction.normalized * enemySpeed * Time.deltaTime);

        if (direction.magnitude <= 0.4f && waypointIndex < EnemyWaypoints.waypoints.Length - 1) {
            waypointIndex++;
        }
        else if (direction.magnitude <= 0.4f) {
            Destroy(gameObject);
            EnemySpawner.enemyDeathEvent.Invoke();
        }

        return direction;
    }

    private void UpdateAnimation(Vector3 movementDirection) {
        animator.SetInteger(actionHash, (int)EnemyAction.Walk);

        float horizontalMagnitude = Mathf.Abs(movementDirection.x);
        float verticalMagnitude = Mathf.Abs(movementDirection.y);

        if (verticalMagnitude > horizontalMagnitude && movementDirection.y < 0) {
            animator.SetInteger(directionHash, (int)EnemyDirection.Down);
        }
        else if (verticalMagnitude > horizontalMagnitude) {
            animator.SetInteger(directionHash, (int)EnemyDirection.Up);
        }
        else {
            animator.SetInteger(directionHash, (int)EnemyDirection.Side);
        }

        spriteRenderer.flipX = movementDirection.x > 0;
    }

    private void FixedUpdate() {
        Vector3 updatedEnemyDirection = MoveEnemy();
        UpdateAnimation(updatedEnemyDirection);
    }
}
