using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAi3 : Vehicle {
    protected override void Awake() {
        base.Awake();
        this.steeringBehaviour.ArriveOn(50f,GameObject.FindGameObjectWithTag("Player").transform,10);
        this.steeringBehaviour.fleePanicOn(100f,GameObject.FindGameObjectWithTag("Player").transform, 10);
        this.steeringBehaviour.SeparationOn(3, 10);
    }
}
