using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpConeShoot : PowerUp {

    public float timeBeforeExpire;
    private float startTime = -1f;

    protected override void PowerUpEffects() {
        startTime = Time.time;
        base.PowerUpEffects();
        player.GetComponent<Shoot>().coneShoot = true;
    }

    private void Update() {
        if (startTime > 0) {
            if (startTime + timeBeforeExpire < Time.time) {
                PowerUpIsFinished();
                startTime = -1f;
            }
            else {
                powerUpUI.UpdateSlot(powerUpName, 1 - (Time.time - startTime) / timeBeforeExpire);
            }
        }

    }

    protected override void PowerUpIsFinished() {
        base.PowerUpIsFinished();
        player.GetComponent<Shoot>().coneShoot = false;
    }

    public override void Reactivate() {
        base.Reactivate();
        if (startTime == -1) {
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
