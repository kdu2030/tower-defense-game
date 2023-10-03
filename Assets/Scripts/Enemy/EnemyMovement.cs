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

    private void MoveEnemy() {
        Transform targetWaypoint = EnemyWaypoints.waypoints[waypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;
        characterController.Move(direction.normalized * enemySpeed * Time.deltaTime);
        if (direction.magnitude <= 0.4f && waypointIndex < EnemyWaypoints.waypoints.Length - 1) {
            waypointIndex++;
        }
    }

    private void Update() {
        MoveEnemy();
        //animator.SetInteger(directionHash, (int)EnemyDirection.Up);
        //animator.SetInteger(actionHash, (int)EnemyAction.Walk);
        //animator.SetInteger(directionHash, (int)EnemyDirection.Side);
    }
}
