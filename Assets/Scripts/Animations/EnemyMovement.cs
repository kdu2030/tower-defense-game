using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    private Animator animator;
    private int directionHash;
    private int actionHash;

    void Start() {
        animator = GetComponent<Animator>();
        directionHash = Animator.StringToHash("Direction");
        actionHash = Animator.StringToHash("Action");
    }

    void Update() {
        //animator.SetInteger(directionHash, (int)EnemyDirection.Up);
        //animator.SetInteger(actionHash, (int)EnemyAction.Walk);
        //animator.SetInteger(directionHash, (int)EnemyDirection.Side);
    }
}
