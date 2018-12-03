using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLife : PowerUp {

    public int gainHealth=10;

    protected override void PowerUpEffects() {
        base.PowerUpEffects();
        player.GetComponent<LifeComponent>().changeLife(-gainHealth);
    }

    private void Update() {

    }

    protected override void PowerUpIsFinished() {
    }

    public override void Reactivate() {
        base.Reactivate();
        PowerUpEffects();
    }

    public override void Assign() {
    }
}
