using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScroller : MonoBehaviour {

    private int scrollSpeed;
    private bool gameStarted;

    // Start is called before the first frame update
    void Start() {
        scrollSpeed = 10;
        gameStarted = false;
    }

    // Update is called once per frame
    void Update() {
        if (!GameManager.isPaused && gameStarted) {
            transform.position -= new Vector3((scrollSpeed * GameManager.gameSpeed) * Time.deltaTime, 0f, 0f);
        }
    }

    public void StartScrolling() {
        gameStarted = true;
    }
}
