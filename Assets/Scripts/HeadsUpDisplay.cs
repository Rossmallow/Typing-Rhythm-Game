using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeadsUpDisplay : MonoBehaviour {

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI mulitplierText;
    public TextMeshProUGUI streakText;

    // Update is called once per frame
    private void Update() {
        scoreText.text = "Score: " + GameManager.score;
    }

    public void SetMultiplier(int multiplier) {
        mulitplierText.text = "Multiplier: x" + multiplier;
    }

    public void SetStreak(int streak) {
        streakText.text = "Streak: " + streak;
    }

}
