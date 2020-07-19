using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class UIObject : MonoBehaviour {

    public TextMeshProUGUI uiNameText;

    public string uiName;
    public int typeIndex = 0;

    public int GetCurrentIndex() {
        return typeIndex;
    }

    public void ResetIndex() {
        typeIndex = 0;
    }

    public string GetName() {
        return uiName;
    }

    public char GetNextLetter() {
        return char.ToLower(uiName[typeIndex]);
    }

    public bool NameTyped() {
        return (typeIndex >= uiName.Length);
    }

    public abstract float TypeLetter();

    public abstract float Backspace();

    public abstract void TurnOffHighlights();
}
