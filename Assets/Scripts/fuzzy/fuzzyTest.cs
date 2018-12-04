using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuzzyTest : MonoBehaviour {

    public AnimationCurve test1;
    public AnimationCurve test2;
    public AnimationCurve test3;

    private void Start() {
        float eval = 0.3f;
        Debug.Log(test1.Evaluate(eval));
        Debug.Log(test2.Evaluate(eval));
        Debug.Log(test3.Evaluate(eval));

        Keyframe[] keys=test1.keys;
        for (int i = 0; i < keys.Length; i++) {
            Debug.Log("t: " + keys[i].time + " y: " + keys[i].value+"out tangent"+ keys[i].outTangent);
        }
}
}
