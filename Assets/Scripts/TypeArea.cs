using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeArea : MonoBehaviour {

    public Sprite defaultSprite, actionSprite;
    public GameObject perfectEffect, hitEffect, missEffect;

    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    //void Update() {
    //    if (Input.anyKey) {
    //        sprite.sprite = actionSprite;
    //    } else {
    //        sprite.sprite = defaultSprite;
    //    }    
    //}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Letter") {
            sprite.sprite = actionSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Letter") {
            sprite.sprite = defaultSprite;
        }
    }

    public void SpawnPerfectEffect() {
        Instantiate(perfectEffect, transform.position, transform.rotation);
    }

    public void SpawnHitEffect() {
        Instantiate(hitEffect, transform.position, transform.rotation);
    }

    public void SpawnMissEffect() {
        Instantiate(missEffect, transform.position, transform.rotation);
    }

    public void TogglePause() {
        if (GameManager.isPaused) {
            GetComponent<CircleCollider2D>().isTrigger = false;
        } else {
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }
}
