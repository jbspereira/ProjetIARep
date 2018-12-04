using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {

    public int score;
    public int newScore;
    TextMeshProUGUI scoreText;

    private void Start() {
        score = GameManage.instance.score;
        newScore = score;
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = "Score : " + score;
    }
    void Update () {
		newScore= GameManage.instance.score;
        if (score < newScore) { //faire avec un coroutine
            score++;
            scoreText.text = "Score : " + score;
        }
    }
}
