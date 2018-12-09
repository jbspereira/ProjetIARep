using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAi4Follower : Vehicle {
    protected override void Awake() {
        base.Awake();
    }

    protected override void setDirection() {
        Vector2 toPlayer=(player.transform.position - transform.position).normalized;
        Vector2 playerVel = player.GetComponent<Rigidbody2D>().velocity;
        float LookAheadTime = toPlayer.magnitude / (maxSpeed + playerVel.magnitude);
        //transform.up = toPlayer + Random.Range(-0.2f, 0.2f) * new Vector2(toPlayer.y,-toPlayer.x);
        Vector2 toPlayerPrediction = toPlayer + playerVel * LookAheadTime;
        transform.up= toPlayerPrediction + Random.Range(-0.2f, 0.2f) * new Vector2(toPlayerPrediction.y, -toPlayerPrediction.x);
    }
}
