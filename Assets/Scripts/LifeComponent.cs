using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComponent : MonoBehaviour {

    public PlayerHealthUI healthUI;

    public int life = 100;
    public bool invicibility = false;
    public void changeLife(int damage) {

        if (damage>0 && invicibility) {
            return;
        }
        life -= damage;
        if (life < 0) {
            life = 0;
            //GameOver();
        }
        else if (life > 100) {
            life = 100;
        }

        healthUI.updateHealth(life);
    }
}
