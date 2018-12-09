﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour {

    public int damageOnCollide = 1;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<LifeComponent>().changeLife(damageOnCollide);
            Destroy(gameObject);
        }
    }
}
