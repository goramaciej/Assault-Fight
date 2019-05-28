using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBoard : MonoBehaviour {

    [SerializeField] string startString = "health: ";
    private int health = 100;
    private TextMeshProUGUI healthText;

    void Awake() {
        healthText = GetComponent<TextMeshProUGUI>();
        SetTextOfHealth();
    }

    public void SetHealth(int _health) {
        health = _health;
        SetTextOfHealth();
    }
    private void SetTextOfHealth() {
        string beforeHealth = startString;
        if (health < 100) {
            beforeHealth = startString + "0";
        } else if (health < 10) {
            beforeHealth = startString + "00";
        } 
        healthText.text = beforeHealth + health.ToString();
    }
}