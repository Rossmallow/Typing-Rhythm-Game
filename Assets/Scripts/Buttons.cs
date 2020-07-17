using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour{

    public GameObject aboutScreen, pauseContents, resultsContents;
    public Slider volumeSlider, speedSlider;

    private bool requestToResume;

    public void GameVolume(float newGameVolume) {
        GameManager.gameVolume = newGameVolume;
    }

    public void GameSpeed(float newGameSpeed) {
        GameManager.gameSpeed = newGameSpeed;
    }

    public void ContinueGame() {
        if (requestToResume == false) {
            requestToResume = true;
        }
    }
    public bool IsRequestingToResume() {
        return requestToResume;
    }

    public void RequestAccepted() {
        requestToResume = false;
    }

    public void RestartGame() {
        SceneManager.LoadScene("Main");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void EnableAboutScreen(bool enable) {
        aboutScreen.SetActive(enable);
        pauseContents.SetActive(!enable);
        resultsContents.SetActive(!enable);
    }

    public void BackButton() {
        aboutScreen.SetActive(false);
        pauseContents.SetActive(true);
        resultsContents.SetActive(true);
    }

}
