using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonObject : UIObject {

    public GameObject[] letterHighlights;

    // Start is called before the first frame update
    private void Start() {
        uiName = uiNameText.text;
        typeIndex = 0;
        TurnOffHighlights();
    }

    public override float TypeLetter() {
        letterHighlights[typeIndex].SetActive(true);
        typeIndex++;
        return -1f;
    }

    public override float Backspace() {
        typeIndex--;
        letterHighlights[typeIndex].SetActive(false);
        return -1f;
    }

    public override void TurnOffHighlights() {
        foreach (GameObject highlight in letterHighlights) {
            highlight.SetActive(false);
        }
    }
}
