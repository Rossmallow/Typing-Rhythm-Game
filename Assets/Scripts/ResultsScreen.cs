using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsScreen : MonoBehaviour {

    public TextMeshProUGUI scoreText, totalNotesText, perfectlyHitNotesText, normallyHitNotesText, missedNotesText, longestStreakText, longestPerfectStreakText;
    public GameObject uiHolder, resultsHolder;

    public void EnableScreen(bool enable) {
        gameObject.SetActive(enable);
        uiHolder.SetActive(enable);
        resultsHolder.SetActive(enable);
    }

    public void SetValues(int totalNotes, int perfectNotes, int normalNotes, int missedNotes, int longestStreak, int longestPerfectStreak) {
        scoreText.text = GameManager.score.ToString();
        totalNotesText.text = totalNotes.ToString();
        perfectlyHitNotesText.text = perfectNotes.ToString();
        normallyHitNotesText.text = normalNotes.ToString();
        missedNotesText.text = missedNotes.ToString();
        longestStreakText.text = longestStreak.ToString();
        longestPerfectStreakText.text = longestPerfectStreak.ToString();
    }
}
