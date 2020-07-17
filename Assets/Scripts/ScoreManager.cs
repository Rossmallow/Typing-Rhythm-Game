using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class ScoreManager : MonoBehaviour {

    public HeadsUpDisplay headsUpDisplay;
    public ResultsScreen resultsScreen;
    
    private int score;
    private int scorePerNote;
    private int scorePerPerfectNote;
    private int scoreMultiplier;
    private int[] multiplierThreasholds;
    private int totalNotes;
    private int normalHits;
    private int perfectHits;
    private int missedNotes;
    private int currentStreak;
    private int currentPerfectStreak;
    private int longestStreak;
    private int longestPrefectStreak;

    // Start is called before the first frame update
    private void Start() {
        score = GameManager.score;
        scorePerNote = 10;
        scorePerPerfectNote = scorePerNote * 2;
        scoreMultiplier = 1;
        multiplierThreasholds = new int[3] {20, 40, 100};
        totalNotes = FindObjectsOfType<LetterObject>().Length;
        normalHits = 0;
        perfectHits = 0;
        missedNotes = 0;
        currentStreak = 0;
        currentPerfectStreak = 0;
        longestStreak = currentStreak;
        longestPrefectStreak = currentPerfectStreak;
        headsUpDisplay.SetMultiplier(scoreMultiplier);
        headsUpDisplay.SetStreak(currentStreak);
        resultsScreen.EnableScreen(false);
    }

    public int AddScore(string hitType = "normal") {
        if (hitType == "perfect") {
            score += scorePerPerfectNote * scoreMultiplier;
            perfectHits++;
            currentPerfectStreak++;
            if (currentPerfectStreak > longestPrefectStreak) {
                longestPrefectStreak = currentPerfectStreak;
            }
        } else {
            score += scorePerNote * scoreMultiplier;
            normalHits++;
        }
        currentStreak++;
        headsUpDisplay.SetStreak(currentStreak);
        if (scoreMultiplier <= multiplierThreasholds.Length && multiplierThreasholds[scoreMultiplier - 1] <= currentStreak) {
            scoreMultiplier++;
            headsUpDisplay.SetMultiplier(scoreMultiplier);
        }
        if (currentStreak > longestStreak) {
            longestStreak = currentStreak;
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
        int missedNotes = totalNotes - perfectHits + normalHits;
        resultsScreen.SetValues(totalNotes, perfectHits, normalHits, missedNotes, longestStreak, longestPrefectStreak);
        resultsScreen.EnableScreen(true);
    }
}
