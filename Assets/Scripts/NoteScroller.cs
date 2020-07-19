using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScroller : MonoBehaviour {

    private int scrollSpeed = 10;
    private bool scrolling = false;

    // Update is called once per frame
    void Update() {
        if (!GameManager.isPaused && scrolling) {
            transform.position -= new Vector3((scrollSpeed * GameManager.gameSpeed) * Time.deltaTime, 0f, 0f);
        }
    }

    public void EnableScrolling(bool enable) {
        scrolling = enable;
    }
}
