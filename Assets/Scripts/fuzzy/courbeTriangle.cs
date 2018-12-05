using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class courbeTriangle{
    float xMin;
    float xCenter;
    float xMax;
    float a1;
    float a2;

    public courbeTriangle(float x1,float x2,float x3) {
        xMin = x1;
        xCenter = x2;
        xMax = x3;
        a1 = 1 / (xCenter - xMin);
        a2 = -1 / ( xMax-xCenter);
    }

    public float Evaluate(float x) {
        if (x<=xMin || x >= xMax) {
            return 0;
        }
        else if (x<xCenter) {
            return a1*x-a1*xMin;
        }
        else  {
            return a2 * x - a2 * xMax;
        }
    }

    public Vector2 Coupe(float c) {
        float p1 = (c + a1 * xMin) / a1;
        float p2 = (c + a2 * xMax) / a2;
        return new Vector2(p1, p2);
    }

}
