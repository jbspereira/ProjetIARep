using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAi2 : Vehicle {
    protected override void Awake() {
        base.Awake();
        this.steeringBehaviour.PursuitOn(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(),10);
        this.steeringBehaviour.WanderOn(100);
        this.steeringBehaviour.SeparationOn(3, 10);
    }
}
