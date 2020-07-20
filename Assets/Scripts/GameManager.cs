using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;
    public static int score;
    public static bool isPaused;
    public static bool isRestarted = false;
    public static float gameSpeed = 1f;
    public static float gameVolume = 0.5f;

    public ScoreManager scoreManager;
    public AudioSource music;
    public NoteScroller noteScroller;
    public PauseScreen pauseScreen;
    public TypeArea typeArea;
    public UserInterface userInterface;

    private bool gameStarted = false;
    private bool gameOver = false;
    private bool unpausing = false;
    private bool restarting = false;

    // Start is called before the first frame update
    private void Start() {
        gameManager = this;
        score = 0;
        isPaused = false;
        UpdateMusic();
        pauseScreen.EnableMenu(false);
        userInterface.EnableAboutScreen(false);
    }

    // Update is called once per frame
    private void Update() {
        if (!gameStarted) {
            if (isRestarted && !restarting) {
                StartCoroutine(Restart());
            } else if (Input.anyKeyDown) {
                    noteScroller.EnableScrolling(true);
                    music.Play();
                    gameStarted = true;
            }
        } else if (!isPaused && !gameOver) {
            if (!music.isPlaying) {
                noteScroller.EnableScrolling(false);
                scoreManager.GameOver();
                gameOver = true;
            } else if (Input.GetKeyDown(KeyCode.Escape)) {
                Pause();
            }
        } else if (isPaused && !unpausing || gameOver) {
            bool resumeRequest = userInterface.IsRequestingToResume();
            if (Input.GetKeyDown(KeyCode.Escape) || resumeRequest) {
                if (userInterface.AboutScreenIsActive() && !resumeRequest) {
                    userInterface.EnableAboutScreen(false);
                } else if (!gameOver) {
                    userInterface.RequestAccepted();
                    StartCoroutine(Resume());
                }
            } else {
                if (Input.GetKeyDown(KeyCode.Tab)) {
                    userInterface.CompleteTyping();
                } else {
                    foreach (char letter in Input.inputString) {
                        userInterface.CheckKey(letter);
                    }
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
        UpdateMusic();
        pauseScreen.EnableMenu(false);
        unpausing = false;
    }
    
    private IEnumerator Restart() {
        restarting = true;
        pauseScreen.EnableMenu(true);
        pauseScreen.EnableUIHolder(false);
        pauseScreen.SetTitleText("Restarting in...");
        pauseScreen.ShowCountdown();
        yield return new WaitForSeconds(3);
        pauseScreen.EnableMenu(false);
        noteScroller.EnableScrolling(true);
        music.Play();
        gameStarted = true;
        restarting = false;
    }

    private void UpdateMusic () {
        music.pitch = gameSpeed;
        music.volume = gameVolume;
    }
}
