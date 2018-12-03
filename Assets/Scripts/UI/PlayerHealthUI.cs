using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour {

    private Image HealthBar;
    private int currentHealth;
    private int newHealth;
    private void Start() {
        currentHealth = 100;
        newHealth = 100;
    }
    private void Awake() {
        HealthBar = GetComponent<Image>();
    }
    public void updateHealth(int targetValue) {
        newHealth = targetValue;
    }

    private void Update() {
        if (currentHealth < newHealth) {
            currentHealth++;
        }
        else if (currentHealth > newHealth) {
            currentHealth--;
        }

        HealthBar.fillAmount = currentHealth / 100f;
    }
}
