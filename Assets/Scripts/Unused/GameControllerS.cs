using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerS : MonoBehaviour {

    public Vehicle Ennemi;
    public int nbEnnemi;
    public int nbEnnemiWander;
    public int carrPop;
    public Vehicle Player;

	void Start () {
		for (int k = 0; k < nbEnnemi; k++) {
            Vector2 position = new Vector2(Random.Range(-carrPop,carrPop), Random.Range(-carrPop, carrPop));
            Vehicle ennemi=Instantiate(Ennemi,position,Quaternion.identity) as Vehicle;
            ennemi.steeringBehaviour.PursuitOn(Player,10);
            ennemi.steeringBehaviour.SeparationOn(2,10);
            ennemi.SetColor(Color.red);
        }

        for (int k = 0; k < nbEnnemi; k++) {
            Vector2 position = new Vector2(Random.Range(-carrPop, carrPop), Random.Range(-carrPop, carrPop));
            Vehicle ennemi = Instantiate(Ennemi, position, Quaternion.identity) as Vehicle;
            ennemi.steeringBehaviour.PursuitOn(Player, 10);
            ennemi.steeringBehaviour.SeparationOn(2, 10);
            ennemi.steeringBehaviour.WanderOn(100);
            ennemi.SetColor(Color.white);
        }
    }
	
	void Update () {
		
	}
}
