using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

public class PauseScreen : MonoBehaviour {

    public TextMeshProUGUI titleText;
    public GameObject countdownAnimation;
    public GameObject buttonsHolder;
    public Slider volumeSlider, speedSlider;

    public void EnableMenu(bool enable) {
        gameObject.SetActive(enable);
        EnableButtonsHolder(enable);
        if (enable) {
            volumeSlider.value = GameManager.gameVolume;
            speedSlider.value = GameManager.gameSpeed;
        }
    }

    public void EnableButtonsHolder(bool enable) {
        buttonsHolder.SetActive(enable);
    }

    public void ShowCountdown() {
        Instantiate(countdownAnimation, new Vector3(0f, 0f, 0f), transform.rotation).transform.SetParent(gameObject.transform, false);
    }

    public void SetTitleText(string newTitle) {
        titleText.text = newTitle;
    }
}
