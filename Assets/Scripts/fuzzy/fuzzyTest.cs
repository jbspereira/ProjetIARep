using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fuzzyTest  {

    public courbeTrapeze notSafe;
    public courbeTriangle safe;
    public courbeTrapeze tooSafe;

    public courbeTrapeze closeJ;
    public courbeTrapeze mediumJ;
    public courbeTrapeze farJ;

    public courbeTrapeze closeI;
    public courbeTriangle mediumI;
    public courbeTrapeze farI;

    public courbeTriangle slowVA;
    public courbeTriangle mediumVA;
    public courbeTrapeze fastVA;

    public fuzzyTest() {
        slowVA = new courbeTriangle(0, 0.2f, 0.4f);
        mediumVA = new courbeTriangle(0.3f, 0.5f, 0.7f);
        fastVA = new courbeTrapeze(0.6f, 0.8f, 1f,10f);

        closeJ = new courbeTrapeze(0,0f,15f,20f);
        mediumJ = new courbeTrapeze(15f,20f, 25f, 30f);
        farJ = new courbeTrapeze(25f, 30f, 9000, 10000);

        closeI = new courbeTrapeze(0, 5, 2.5f, 7.5f);
        mediumI = new courbeTriangle(2.5f, 7.5f, 15f);
        farI = new courbeTrapeze(7.5f, 15f, 9000, 10000);

        notSafe = new courbeTrapeze(0, 5, 25f, 50f);
        safe = new courbeTriangle(25f, 50f, 75f);
        tooSafe = new courbeTrapeze(50f, 75f, 95f, 100f);


        //float res=fuzzyCalculate(15f,50f,0.1f);
        //Debug.Log("logique floue " + res);
    }

    public float fuzzyCalculate(float distanceJ,float distanceI,float vitesseNorm) {
        float closeJ_var = closeJ.Evaluate(distanceJ);
        float mediumJ_var = mediumJ.Evaluate(distanceJ);
        float farJ_var = farJ.Evaluate(distanceJ);

        float closeI_var = closeI.Evaluate(distanceI);
        float mediumI_var = mediumI.Evaluate(distanceI);
        float farI_var = farI.Evaluate(distanceI);

        float slowVA_var = slowVA.Evaluate(vitesseNorm);
        float mediumVA_var = mediumVA.Evaluate(vitesseNorm);
        float fastVA_var = fastVA.Evaluate(vitesseNorm);

        float maxNotSafe_var=0;
        float maxSafe_var=0;
        float maxTooSafe_var=0;

        //fuzzy rules
        if (closeJ_var > maxNotSafe_var)
            maxNotSafe_var = closeJ_var;
        if (farJ_var > maxTooSafe_var)
            maxTooSafe_var = farJ_var;
        if (Mathf.Min(mediumJ_var, closeI_var) > maxNotSafe_var)
            maxNotSafe_var = Mathf.Min(mediumJ_var, closeI_var);
        if (Mathf.Min(mediumJ_var, fastVA_var) > maxNotSafe_var)
            maxNotSafe_var = Mathf.Min(mediumJ_var, fastVA_var);
        if (Mathf.Min(mediumJ_var, Mathf.Max(mediumI_var,farI_var)) > maxSafe_var)
            maxSafe_var = Mathf.Min(mediumJ_var, Mathf.Max(mediumI_var, farI_var));
        if (Mathf.Min(mediumJ_var, Mathf.Max(slowVA_var, mediumVA_var)) > maxSafe_var)
            maxSafe_var = Mathf.Min(mediumJ_var, Mathf.Max(slowVA_var, mediumVA_var));

        //defuzzifier
        float max = 0;
        int indiceMax = 0;
        float[] tab = { maxNotSafe_var, maxSafe_var , maxTooSafe_var };
        for (int i = 0; i < 3; i++) {
            if (tab[i] > max) {
                max = tab[i];
                indiceMax = i;
            }
        }
        //Debug.Log("max " + maxNotSafe_var+ "    "+ maxSafe_var + "     "+ maxTooSafe_var);
        //Debug.Log("max "+max);
        Vector2 final;
        switch (indiceMax) {
            case 0:
                final=notSafe.Coupe(max);
                break;
            case 1:
                final=safe.Coupe(max);
                break;
            default:
                final=tooSafe.Coupe(max);
                break;
        }
        return (final.y+final.x)/2f;
    }
}
