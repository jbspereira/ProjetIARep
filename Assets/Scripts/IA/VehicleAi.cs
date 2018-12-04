using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAi : Vehicle {
    float currentDistance;
    Animator animator;
    private GameObject player;
    protected override void Awake() {
        base.Awake();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        //this.steeringBehaviour.PursuitOn(player.GetComponent<PlayerController>(),10);
        
    }

    private void Update() {
        LifeComponent playerLife = player.GetComponent<LifeComponent>();

        currentDistance = Vector3.Distance(player.transform.position, transform.position);
        animator.SetFloat("distanceFromPlayer", currentDistance);

        animator.SetBool("isInvincible", playerLife.invicibility);
    }

    public void FleeEnter() {
        this.steeringBehaviour.reset();
        this.steeringBehaviour.FleeOn(player.transform, 10);
        this.steeringBehaviour.SeparationOn(3, 10);
    }

    public void ChaseEnter() {
        this.steeringBehaviour.reset();
        this.steeringBehaviour.PursuitOn(player.GetComponent<PlayerController>(), 10);
        this.steeringBehaviour.SeparationOn(3, 10);
    }

    public void IniEnter() {
        this.steeringBehaviour.reset();
        this.steeringBehaviour.PursuitOn(player.GetComponent<PlayerController>(), 10);
        this.steeringBehaviour.SeparationOn(3, 10);
    }
}
