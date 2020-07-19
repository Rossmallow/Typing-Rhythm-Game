using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class ScoreManager : MonoBehaviour {

    public HeadsUpDisplay headsUpDisplay;
    public ResultsScreen resultsScreen;
    
    private int score = GameManager.score;
    private int scorePerNote = 10;
    private int scorePerPerfectNote;
    private int scoreMultiplier = 1;
    private int[] multiplierThreasholds = new int[] { 20, 40, 100 };
    private int totalNotes;
    private int normalHits = 0;
    private int perfectHits = 0;
    private int missedNotes = 0;
    private int currentStreak = 0;
    private int currentPerfectStreak = 0;
    private int longestStreak = 0;
    private int longestPrefectStreak = 0;

    // Start is called before the first frame update
    private void Start() {
        scorePerPerfectNote = scorePerNote * 2;
        totalNotes = FindObjectsOfType<LetterObject>().Length;
        headsUpDisplay.SetMultiplier(scoreMultiplier);
        headsUpDisplay.SetStreak(currentStreak);
        resultsScreen.EnableScreen(false);
    }

    public int AddScore(string hitType = "normal") {
        if (hitType == "perfect") {
            score += scorePerPerfectNote * scoreMultiplier;
        } else {
            score += scorePerNote * scoreMultiplier;
        }

        if (hitType == "perfect") {
            perfectHits++;
            currentPerfectStreak++;
            if (currentPerfectStreak > longestPrefectStreak) {
                longestPrefectStreak = currentPerfectStreak;
            }
        } else {
            normalHits++;
        }
        currentStreak++;
        headsUpDisplay.SetStreak(currentStreak);
        if (currentStreak > longestStreak) {
            longestStreak = currentStreak;
        }

        if (scoreMultiplier <= multiplierThreasholds.Length && multiplierThreasholds[scoreMultiplier - 1] <= currentStreak) {
            scoreMultiplier++;
            headsUpDisplay.SetMultiplier(scoreMultiplier);
        }

        return score;
    }

    public void ResetStreak() {
        missedNotes++;
        currentStreak = 0;
        headsUpDisplay.SetStreak(currentStreak);
        scoreMultiplier = 1;
        headsUpDisplay.SetMultiplier(scoreMultiplier);
        currentPerfectStreak = 0;
    }

    public void GameOver() {
        resultsScreen.SetValues(totalNotes, perfectHits, normalHits, missedNotes, longestStreak, longestPrefectStreak);
        resultsScreen.EnableScreen(true);
    }
}
