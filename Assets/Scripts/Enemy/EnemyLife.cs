using UnityEngine;

public class EnemyLife : MonoBehaviour {
    [Header("Attributes")]
    [SerializeField] private int enemyLives = 5;

    public int LivesRemaining { get; set; }


    private void Start() {
        LivesRemaining = enemyLives;
    }

    public int UpdateLivesRemaining(int damage) {
        LivesRemaining -= damage;
        if (LivesRemaining <= 0) {
            EnemySpawner.enemyDeathEvent.Invoke();
            Destroy(gameObject);
        }
        return LivesRemaining;
    }
}
