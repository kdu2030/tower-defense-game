using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyLife : MonoBehaviour {
    [Header("Attributes")]
    [SerializeField] private float enemyLives = 5;
    [SerializeField] private GameObject healthbarObject;

    public float LivesRemaining { get; set; }
    private Animator animator;
    private Healthbar healthbar;

    private int actionHash;

    private void Start() {
        LivesRemaining = enemyLives;
        animator = GetComponent<Animator>();
        actionHash = Animator.StringToHash("Action");

        healthbar = healthbarObject.GetComponent<Healthbar>();
        healthbar.MaxHealth = enemyLives;
    }

    private void SetEnemyAnimation() {
        EnemySpawner.enemyDeathEvent.Invoke();
        animator.SetInteger(actionHash, (int)EnemyAction.Death);
    }

    public void RemoveEnemy() {
        Destroy(gameObject);
    }

    public float UpdateLivesRemaining(float damage) {
        LivesRemaining -= damage;
        healthbar.UpdateHealthBar(LivesRemaining);

        if (!healthbarObject.activeSelf) {
            healthbarObject.SetActive(true);
        }

        if (animator.GetInteger(actionHash) != (int)EnemyAction.Death && LivesRemaining <= 0) {
            SetEnemyAnimation();
        }
        return LivesRemaining;
    }
}
