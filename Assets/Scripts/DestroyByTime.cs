using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

    public static float timeAvantDestruction=1f;
    private float startTime;

    void Start () {
        Destroy(gameObject, timeAvantDestruction);
        startTime = Time.time;
        gameObject.GetComponent<SpriteRenderer>().color =Color.red;
    }

    private void Update() {
        if (Time.time - startTime > timeAvantDestruction-0.5f) {
            Color color = Color.red;
            color.a = Mathf.Lerp(1, 0,(Time.time-startTime- timeAvantDestruction+0.5f) /(0.5f));
            GetComponent<SpriteRenderer>().color =color;
        }
    }
}
