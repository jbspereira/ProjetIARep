using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController:Vehicle  {

    protected override void Awake() {
        base.Awake();
        this.steeringBehaviour.PlayerOn(10); //c'est un joueur!!
    }

    protected override void setDirection() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = new Vector2(mousePos.x - rb.position.x, mousePos.y - rb.position.y);
    }
}
