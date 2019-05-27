using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBoard : MonoBehaviour {

    [SerializeField] string startString = "score: ";
    private int score = 0;
    private TextMeshProUGUI scoreText;
    
    void Awake() {
        scoreText = GetComponent<TextMeshProUGUI>();
        SetTextOfScore();
    }

    public void AddScore(int _score) {
        score += _score;
        SetTextOfScore();
    }
    private void SetTextOfScore() {
        string beforeScore = "";
        if (score < 10) {
            beforeScore = startString + "000";
        }else if (score < 100) {
            beforeScore = startString + "00";
        }else if (score < 1000) {
            beforeScore = startString + "0";
        }
        scoreText.text = beforeScore + score.ToString();
    }
}
