using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;
    public static int score = 0;
    public static bool isPaused = false;
    public static float gameSpeed = 1f;
    public static float gameVolume = 0.5f;

    public ScoreManager scoreManager;
    public AudioSource music;
    public NoteScroller noteScroller;
    public PauseScreen pauseScreen;
    public GameObject aboutScreen;
    public TypeArea typeArea;
    public UserInterface userInterface;

    private bool gameStarted = false;
    private bool gameOver = false;
    private bool unpausing = false;

    // Start is called before the first frame update
    private void Start() {
        gameManager = this;
        UpdateMusic();
        pauseScreen.EnableMenu(false);
        userInterface.EnableAboutScreen(false);
    }

    // Update is called once per frame
    private void Update() {
        if (!gameStarted) {
            if (Input.anyKeyDown) {
                gameStarted = true;
                noteScroller.EnableScrolling(true);
                music.Play();
            }
        } else {
            if (!music.isPlaying && !gameOver) {
                gameOver = true;
                noteScroller.EnableScrolling(false);
                scoreManager.GameOver();
            }
            if (aboutScreen.activeSelf && Input.GetKeyDown(KeyCode.Escape)) {
                userInterface.EnableAboutScreen(false);
            } else if (isPaused && !unpausing) {
                if (Input.GetKeyDown(KeyCode.Escape) || userInterface.IsRequestingToResume()) {
                    userInterface.RequestAccepted();
                    StartCoroutine(Resume());
                }
            } 
            if (!isPaused) {
                if (Input.GetKeyDown(KeyCode.Escape) && !gameOver) {
                    Pause();
                }
            }
            if (gameOver || (isPaused && !unpausing)) {
                foreach (char letter in Input.inputString) {
                    userInterface.CheckKey(letter);
                }
            }
        }
    }

    public void LetterHit(float pos) {
        if (Mathf.Abs(pos) <= 0.3) {
            typeArea.SpawnPerfectEffect();
            score = scoreManager.AddScore("perfect");
        } else {
            typeArea.SpawnHitEffect();
            score = scoreManager.AddScore();
        }
    }

    public void LetterMissed() {
        typeArea.SpawnMissEffect();
        scoreManager.ResetStreak();
    }

    private void Pause() {
        isPaused = true;
        typeArea.TogglePause();
        music.pitch = 0;
        pauseScreen.SetTitleText("Paused");
        userInterface.SetSlider("Volume", gameVolume);
        userInterface.SetSlider("Speed", gameSpeed);
        pauseScreen.EnableMenu(true);
    }

    private IEnumerator Resume() {
        unpausing = true;
        pauseScreen.EnableUIHolder(false);
        pauseScreen.SetTitleText("Continuing in...");
        pauseScreen.ShowCountdown();
        yield return new WaitForSeconds(3);
        isPaused = false;
        typeArea.TogglePause();
        UpdateMusic();
        pauseScreen.EnableMenu(false);
        unpausing = false;
    }
    
    private void UpdateMusic () {
        music.pitch = gameSpeed;
        music.volume = gameVolume;
    }
}
