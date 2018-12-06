using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAi3 : Vehicle {
    Animator animator;
    fuzzyTest fuzzy;
    protected override void Awake() {
        base.Awake();
        fuzzy =new fuzzyTest();
        animator = GetComponent<Animator>();
        /*this.steeringBehaviour.ArriveOn(50f,GameObject.FindGameObjectWithTag("Player").transform,10);
        this.steeringBehaviour.fleePanicOn(100f,GameObject.FindGameObjectWithTag("Player").transform, 10);
        this.steeringBehaviour.SeparationOn(3, 10);*/
    }

    public override void FleeEnter() {
        maxSpeed = 8;
        base.FleeEnter();
    }

    public override void ChaseEnter() {
        maxSpeed = 8;
        base.ChaseEnter();
    }

    public override void IniEnter() {
        base.IniEnter();
    }

    private void updateFuzzy() {
        Vector2 toPlayer = GameManage.instance.playerPos - rb.position;
        float value=(Vector2.Dot(toPlayer, GameManage.instance.playerVelocity)+15)/30;
        animator.SetFloat("fuzzyRes", fuzzy.fuzzyCalculate(toPlayer.magnitude, GameManage.instance.distToInv, value));
    }

    public override void chaseUpdate() {
        updateFuzzy();
        return;
    }

    public override void fleeUpdate() {
        updateFuzzy();
        return;
    }
    public override void ShootEnter() {
        maxSpeed = 0;
        base.ShootEnter();
    }
    public override void updateTransi() {
        updateFuzzy();
    }
}
