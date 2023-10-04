using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    private Animator animator;
    private CharacterController characterController;
    private int directionHash;
    private int actionHash;
    private int waypointIndex = 0;

    [SerializeField]
    private float enemySpeed;

    private void Start() {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        directionHash = Animator.StringToHash("Direction");
        actionHash = Animator.StringToHash("Action");
    }

    private Vector3 MoveEnemy() {
        Transform targetWaypoint = EnemyWaypoints.waypoints[waypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;
        characterController.Move(direction.normalized * enemySpeed * Time.deltaTime);
        if (direction.magnitude <= 0.4f && waypointIndex < EnemyWaypoints.waypoints.Length - 1) {
            waypointIndex++;
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
            // TODO: Add check for left right and flip animation
            animator.SetInteger(directionHash, (int)EnemyDirection.Side);
        }
    }

    private void Update() {
        Vector3 updatedEnemyDirection = MoveEnemy();
        UpdateAnimation(updatedEnemyDirection);
    }
}
