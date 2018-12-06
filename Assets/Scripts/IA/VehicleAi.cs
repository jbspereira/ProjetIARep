using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAi : Vehicle {
    float currentDistance;
    Animator animator;
    protected override void Awake() {
        base.Awake();
        animator = GetComponent<Animator>();
        //this.steeringBehaviour.PursuitOn(player.GetComponent<PlayerController>(),10);
        
    }

    private void Update() {
        LifeComponent playerLife = player.GetComponent<LifeComponent>();

        currentDistance = Vector3.Distance(player.transform.position, transform.position);
        animator.SetFloat("distanceFromPlayer", currentDistance);

        animator.SetBool("isInvincible", playerLife.invicibility);
    }

    public override void FleeEnter() {
        base.FleeEnter();
    }

    public override void ChaseEnter() {
        base.ChaseEnter();
    }

    public override void IniEnter() {
        base.IniEnter();
    }
}
