using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LetterObject : MonoBehaviour {

    public KeyCode alphaNumeric;
    public bool shiftModifier;

    private SpriteRenderer spriteRenderer;
    private bool canBePressed;

    // Start is called before the first frame update
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    private void Update() {
        if (canBePressed && Input.GetKeyDown(alphaNumeric)) {
            if ((shiftModifier && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) ||
                (!shiftModifier && (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)))) {
                GameManager.gameManager.LetterHit(transform.position.x, alphaNumeric);
                DisableLetter();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "GameRenderer") {
            spriteRenderer.enabled = true;
        }
        if (other.tag == "TypeArea" && gameObject.activeSelf) {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "TypeArea" && spriteRenderer.isVisible) {
            canBePressed = false;
            GameManager.gameManager.LetterMissed(alphaNumeric);
        }
        if (other.tag == "GameRenderer") {
            Destroy(gameObject);
        }
    }

    public void DisableLetter() {
        spriteRenderer.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
