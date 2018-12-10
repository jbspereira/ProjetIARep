﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTimeAI : MonoBehaviour {

    public static float timeAvantDestruction = 2f;
    private float startTime;
    private Color colorGO;

    void Start() {
        Destroy(gameObject, timeAvantDestruction);
        startTime = Time.time;
        colorGO = gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void Update() {
        if (Time.time - startTime > timeAvantDestruction - 0.5f) {
            Color color = colorGO;
            color.a = Mathf.Lerp(1, 0, (Time.time - startTime - timeAvantDestruction + 0.5f) / (0.5f));
            colorGO = color;
        }
    }
}
