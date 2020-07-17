using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;
    public static int score;
    public static bool isPaused;
    public static float gameSpeed;
    public static float gameVolume;

    public ScoreManager scoreManager;
    public AudioSource music;
    public NoteScroller noteScroller;
    public PauseScreen pauseScreen;
    public GameObject aboutScreen;
    public TypeArea typeArea;
    public Buttons buttons;

    private bool gameStarted;
    private bool gameOver;
    private bool unpausing;

    // Start is called before the first frame update
    private void Start() {
        gameManager = this;
        isPaused = false;
        gameSpeed = 1f;
        gameVolume = 0.5f;
        UpdateMusic();
        gameStarted = false;
        unpausing = false;
        pauseScreen.EnableMenu(isPaused);
        buttons.EnableAboutScreen(false);
    }

    // Update is called once per frame
    private void Update() {
        if (!gameStarted) {
            if (Input.anyKeyDown) {
                gameStarted = true;
                noteScroller.StartScrolling();
                music.Play();
            }
        } else {
            if (!music.isPlaying && !gameOver) {
                gameOver = true;
                scoreManager.GameOver();
            }
            if (aboutScreen.activeSelf && Input.GetKeyDown(KeyCode.Escape)) {
                buttons.EnableAboutScreen(false);
            } else if (isPaused && !unpausing && !gameOver) {
                if (Input.GetKeyDown(KeyCode.Escape) || buttons.IsRequestingToResume()) {
                    buttons.RequestAccepted();
                    StartCoroutine(Resume());
                }
            } else if (!isPaused) {
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    Pause();
                }
            }
        }
    }

    public void LetterHit(float pos, KeyCode code) {
        if (Mathf.Abs(pos) <= 0.3) {
            typeArea.SpawnPerfectEffect();
            score = scoreManager.AddScore("perfect");
        } else {
            typeArea.SpawnHitEffect();
            score = scoreManager.AddScore();
        }
        Debug.Log("Hit " + code + " " + pos);
    }

    public void LetterMissed(KeyCode code) {
        typeArea.SpawnMissEffect();
        scoreManager.ResetStreak();
        Debug.Log("Missed " + code);
    }

    private void Pause() {
        isPaused = true;
        typeArea.TogglePause();
        music.pitch = 0;
        pauseScreen.SetTitleText("Paused");
        pauseScreen.EnableMenu(isPaused);
    }

    private IEnumerator Resume() {
        unpausing = true;
        pauseScreen.EnableButtonsHolder(false);
        pauseScreen.SetTitleText("Continuing in...");
        pauseScreen.ShowCountdown();
        yield return new WaitForSeconds(3);
        isPaused = false;
        typeArea.TogglePause();
        UpdateMusic();
        pauseScreen.EnableMenu(isPaused);
        unpausing = false;
    }
    
    private void UpdateMusic () {
        music.pitch = gameSpeed;
        music.volume = gameVolume;
    }
}
