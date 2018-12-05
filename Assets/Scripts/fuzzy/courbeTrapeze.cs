using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class courbeTrapeze {
    float xMin;
    float xCenterG;
    float xCenterD;
    float xMax;
    float a1;
    float a2;

    public courbeTrapeze(float x1, float x2, float x3,float x4) {
        xMin = x1;
        xCenterG = x2;
        xCenterD = x3;
        xMax = x4;
        a1 = 1 / (xCenterG - xMin);
        a2 = -1 / (xMax - xCenterD);
    }

    public float Evaluate(float x) {
        if (x <= xMin || x >= xMax) {
            return 0;
        }
        else if (x>=xCenterG && x<= xCenterD) {
            return 1;
        }
        else if (x < xCenterG) {
            return a1 * x - a1 * xMin;
        }
        else {
            return a2 * x - a2 * xMax;
        }
    }

    public Vector2 Coupe(float c) {
        float p1 = (c + a1 * xMin) / a1;
        float p2 = (c + a2 * xMax) / a2;
        return new Vector2(p1, p2);
    } 

}
