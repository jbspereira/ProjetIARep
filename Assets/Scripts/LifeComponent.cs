using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComponent : MonoBehaviour {

    public PlayerHealthUI healthUI;
    public Pause pauseUI;

    private int life ;
    public bool invicibility = false;

    private void Start() {
        life = GameManage.instance.playerLife;
        //Debug.Log(life);
        healthUI.setLife(life);
    }

    public void changeLife(int damage) {

        if (damage>0 && invicibility) {
            return;
        }
        life -= damage;
        if (life < 0) {
            life = 0;
            pauseUI.OnGameOver();
            //GameOver();
        }
        else if (life > 100) {
            life = 100;
        }
        GameManage.instance.playerLife=life;
        healthUI.updateHealth(life);
    }
}
