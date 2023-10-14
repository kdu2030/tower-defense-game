using UnityEngine;

public class EnemyLife : MonoBehaviour {
    [Header("Attributes")]
    [SerializeField] private int enemyLives = 5;

    public int LivesRemaining { get; set; }
    private Animator animator;
    private int actionHash;


    private void Start() {
        LivesRemaining = enemyLives;
        animator = GetComponent<Animator>();
        actionHash = Animator.StringToHash("Action");
    }

    private void SetEnemyAnimation() {
        EnemySpawner.enemyDeathEvent.Invoke();
        animator.SetInteger(actionHash, (int)EnemyAction.Death);
    }

    public void RemoveEnemy() {
        Destroy(gameObject);
    }

    public int UpdateLivesRemaining(int damage) {
        LivesRemaining -= damage;
        //if (animator.GetInteger(actionHash) != (int)EnemyAction.Death && LivesRemaining <= 0) {
        //    SetEnemyAnimation();
        //}
        if (LivesRemaining <= 0) {
            //Destroy(gameObject);
        }
        return LivesRemaining;

    }
}
