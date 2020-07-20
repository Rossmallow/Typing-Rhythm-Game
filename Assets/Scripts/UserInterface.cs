using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour {

    public GameObject aboutScreen, pauseContents, resultsContents;
    public Slider volumeSlider, speedSlider;

    private bool requestToResume = false;
    private bool hasactiveUI = false;
    private string activeName;
    private UIObject activeUI;
    private UIObject[] uiObjects;

    public void SetGameVolume(float newGameVolume) {
        GameManager.gameVolume = newGameVolume;
    }

    public void SetGameSpeed(float newGameSpeed) {
        GameManager.gameSpeed = newGameSpeed;
    }

    public void SetSlider(string sliderToSet, float newSliderValue) {
        if (sliderToSet == "Volume") {
            volumeSlider.value = newSliderValue;
        } else if (sliderToSet == "Speed") {
            speedSlider.value = newSliderValue;
        }
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
        GameManager.isRestarted = true;
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

    public bool AboutScreenIsActive() {
        return aboutScreen.activeSelf;
    }

    public void CheckKey(char letter) {
        if (letter == '\r' || letter == '\n') {
            ExecuteUI();
        } else if (letter == '\b') {
            Backspace();
        } else {
            letter = char.ToLower(letter);
            uiObjects = FindObjectsOfType<UIObject>();
            if (hasactiveUI) {
                if (!activeUI.NameTyped() && activeUI.GetNextLetter() == letter) {
                    SetSlider(activeName, activeUI.TypeLetter());
                }
            } else {
                foreach (UIObject UI in uiObjects) {
                    if (UI.GetNextLetter() == letter) {
                        activeUI = UI;
                        activeName = activeUI.GetName();
                        hasactiveUI = true;
                        SetSlider(activeName, activeUI.TypeLetter());
                        break;
                    }
                }
            }
        }
    }

    public void CompleteTyping() {
        if (hasactiveUI) {
            SetSlider(activeName, activeUI.EnableAllHighlights(true));
        }
    }

    private void ExecuteUI () {
        if (hasactiveUI) {
            switch (activeName) {
                case "Volume":
                    SetGameVolume(activeUI.GetComponent<Slider>().value);
                    break;
                case "Speed":
                    SetGameSpeed(activeUI.GetComponent<Slider>().value);
                    break;
                case "Continue":
                    ContinueGame();
                    break;
                case "Restart":
                    RestartGame();
                    break;
                case "Quit":
                    QuitGame();
                    break;
                case "About":
                    EnableAboutScreen(true);
                    break;
                case "Back":
                    EnableAboutScreen(false);
                    break;
            }
            activeUI.EnableAllHighlights(false);
            activeUI.ResetIndex();
            hasactiveUI = false;
        }
    }

    private void Backspace() {
        if (hasactiveUI) {
            SetSlider(activeName, activeUI.Backspace());
            if (activeUI.GetCurrentIndex() == 0) {
                hasactiveUI = false;
            }
        }
    }

}
