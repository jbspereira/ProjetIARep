﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Vehicle : MonoBehaviour {

    [Serializable]
    public class WanderParameters {
        public float radius;
        public float distance;
        public float jitter;
    }

    public float maxSpeed=5;
    [HideInInspector] public float elapsedTime;
    public SteeringBehaviour steeringBehaviour;
    protected Rigidbody2D rb;
    protected Vector2 acceleration;
    public WanderParameters wanderParam = new WanderParameters();
    protected static GameObject player;
    void FixedUpdate() {
        elapsedTime = Time.fixedDeltaTime;

        Vector2 resultanteForces = new Vector2(0, 0);
        setDirection();

        //Combiner les forces ici
        resultanteForces += steeringBehaviour.Calculate();
        Debug.DrawLine(rb.position,rb.position+resultanteForces);
        //Finalement appliquer la resultante
        acceleration = resultanteForces;
        Vector2 vel = rb.velocity + resultanteForces * elapsedTime;
        if (vel.magnitude > maxSpeed) {
            rb.velocity = vel.normalized * maxSpeed;
        }
        else
            rb.velocity = vel;
    }

    public Vector2 getPosition() {
        return rb.position;
    }

    public Vector2 getVelocity() {
        return rb.velocity;
    }

    public void setVelocity(Vector2 vel) {
        rb.velocity=vel;
    }

    public Vector2 Heading() {
        return getVelocity().normalized;
    }

    public Vector2 Side() {
        Vector2 head = Heading();
        return new Vector2(head.y, -head.x);
    }

    public void SetColor(Color color) {
        GetComponent<SpriteRenderer>().color = color;
    }

    protected virtual void Awake() {
        if (player==null)
            player = GameObject.FindGameObjectWithTag("Player");
        acceleration = Vector2.zero;
        steeringBehaviour = new SteeringBehaviour(this, wanderParam.radius, wanderParam.distance, wanderParam.jitter);
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void setDirection() {// pure virtual must be overridden
        
        if (rb.velocity.sqrMagnitude != 0)
            transform.up = rb.velocity.normalized; //oriente le vehicule vers la cible
        else if (player != null)
            transform.up = (player.transform.position - transform.position).normalized;
    }

    public virtual void FleeEnter() {
        this.steeringBehaviour.reset();
        maxSpeed = 8.0f;
        this.steeringBehaviour.FleeOn(player.transform, 10);
        this.steeringBehaviour.SeparationOn(3, 10);
    }

    public virtual void ChaseEnter() {
        this.steeringBehaviour.reset();
        maxSpeed = 8.0f;
        this.steeringBehaviour.PursuitOn(player.GetComponent<PlayerController>(), 10);
        this.steeringBehaviour.SeparationOn(3, 10);
    }

    public virtual void IniEnter() {
        //this.steeringBehaviour.reset();
        //this.steeringBehaviour.PursuitOn(player.GetComponent<PlayerController>(), 10);
        maxSpeed = 3.0f;
        //this.steeringBehaviour.SeparationOn(3, 10);
    }
    public virtual void chaseUpdate() {
        this.steeringBehaviour.SeparationOn(3, 10);
    }
    public virtual void fleeUpdate() {
        this.steeringBehaviour.SeparationOn(3, 10);
    }
    public virtual void ShootEnter() {
        this.steeringBehaviour.reset();
    }
    public virtual void updateTransi() {
        this.steeringBehaviour.SeparationOn(3, 10);
    }
}
