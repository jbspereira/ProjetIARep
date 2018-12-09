using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAi4Leader : Vehicle {

    public GameObject follower;
    protected override void Awake() {
        base.Awake();

        this.steeringBehaviour.PursuitTangentOn(player.GetComponent<PlayerController>(), 10,20,10,20);
        this.steeringBehaviour.WanderOn(100);

        Vehicle lastVehicle = this;
        for (int i = 0; i < 6; i++) {
            VehicleAi4Follower followerVehicle = Instantiate(follower).GetComponent<VehicleAi4Follower>();
            followerVehicle.steeringBehaviour.OffsetPursuitOn(lastVehicle,new Vector2 (0,-1f),1.0f,10);
            lastVehicle = followerVehicle;
        }
        //this.steeringBehaviour.WanderOn(100);ff
    }
}
