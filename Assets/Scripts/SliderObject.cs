using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderObject : UIObject {

    public float[] sliderValues;

    // Start is called before the first frame update
    void Start() {
        uiName = uiNameText.text;
        typeIndex = 0;
    }

    public override float TypeLetter() {
        return sliderValues[typeIndex++];
    }

    public override float Backspace() {
        typeIndex--;
        if (typeIndex > 0) {
            return sliderValues[typeIndex - 1];
        } else {
            return 0f;
        }
    }

    public override float EnableAllHighlights(bool enable) {
        if (enable) {
            typeIndex = uiName.Length;
            return sliderValues[typeIndex - 1];
        }
        return -1f;
    }
}
