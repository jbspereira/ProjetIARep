using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAi2 : Vehicle {
    protected override void Awake() {
        base.Awake();
        this.steeringBehaviour.PursuitOn(player.GetComponent<PlayerController>(),10);
        this.steeringBehaviour.WanderOn(50);
        this.steeringBehaviour.SeparationOn(3, 10);

    }
}
