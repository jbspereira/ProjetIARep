using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : PowerUp {

    public float timeBeforeExpire;
    public float boostSpeed;
    private float startTime = -1f;
    private PlayerController playerController;

    protected override void PowerUpEffects() {
        playerController = player.GetComponent<PlayerController>();
        startTime = Time.time;
        base.PowerUpEffects();
        Debug.Log("boost");
        playerController.maxSpeed += boostSpeed;
    }

    private void Update() {
        if (startTime > 0) {
            if (startTime + timeBeforeExpire < Time.time) {
                Debug.Log(startTime + "  " + timeBeforeExpire + "  " + Time.time);
                PowerUpIsFinished();
                startTime = -1f;
            }
            else {
                powerUpUI.UpdateSlot(powerUpName,1-(Time.time-startTime)/ timeBeforeExpire);
            }
        }

    }

    protected override void PowerUpIsFinished() {
        base.PowerUpIsFinished();
        Debug.Log("deboost");
        playerController.maxSpeed -= boostSpeed;
    }

    public override void Reactivate() {
        base.Reactivate();
        if (startTime == -1){
            PowerUpEffects();
        }
        else {
            startTime = Time.time;
        }
    }

    public override void Assign() {
        base.Assign();
    }
}
