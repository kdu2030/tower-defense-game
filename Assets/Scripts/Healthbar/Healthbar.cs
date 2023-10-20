using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {
    [Header("References")]
    [SerializeField] private bool showHealthBarAtStart;

    public float MaxHealth { get; set; }
    public float Health { get; set; }
    private Slider slider;

    private void Start() {
        slider = GetComponent<Slider>();
        Health = MaxHealth;
        gameObject.SetActive(showHealthBarAtStart);
    }

    public void UpdateHealthBar(float health) {
        Health = health;
        slider.value = health / MaxHealth;
    }
}
