using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PowerUp : MonoBehaviour {

    public string powerUpName;

    protected PowerUpUI powerUpUI;
    protected GameObject player;
    protected SpriteRenderer spriteRenderer;
    protected Collider2D colliderComp;
    protected void Awake() {
        powerUpUI = FindObjectOfType<PowerUpUI>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colliderComp = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject goCollecting = collision.gameObject;
        CollectPowerUp(goCollecting);
    }

    protected virtual void PowerUpEffects() {
        //effet du power up à implémenter dans classes filles
    }

    protected virtual void CollectPowerUp(GameObject goCollecting) {
        if (goCollecting.tag != "Player") {
            return;
        }
        //Debug.Log("Power Up ramassé player");
        player = goCollecting;

        if (player.GetComponent<PowerUpManager>().AddPowerUp(this)) {
            PowerUpEffects();
        }
        spriteRenderer.enabled = false;
        colliderComp.enabled = false;
    }

    protected virtual void PowerUpIsFinished() {
        powerUpUI.PowerUpFinished(powerUpName);
    }
    public virtual void Reactivate() {
        //reactivation du powerUp
    }
    public virtual void Assign() {
        powerUpUI.AssignSlotToPowerUp(powerUpName,GetComponent<SpriteRenderer>().color);
    }
}
