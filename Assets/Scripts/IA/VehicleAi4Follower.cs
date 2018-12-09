using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAi4Follower : Vehicle {
    protected override void Awake() {
        base.Awake();
    }

    protected override void setDirection() {
         Vector2 toPlayer=(player.transform.position - transform.position).normalized;
        transform.up = toPlayer + Random.Range(-0.2f, 0.2f) * new Vector2(toPlayer.y,-toPlayer.x);
    }
}
