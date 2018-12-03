using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DestroyPlayer : MonoBehaviour {

    public int damageOnCollide = 1;
    private bool contact=false;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<LifeComponent>().changeLife(damageOnCollide);
            contact = true;
        }
        else if (collision.gameObject.tag == "bullet") {
            Destroy(collision.gameObject);
            contact = true;
            
        }
    }

    private void Update() { //on fait ça dans le update sinon les messages sont parfois envoyés deux fois dans le cas où un tir et le joueur touchent un ennemi durant la meme frame
        if (contact) {
            if (GameManage.instance!=null)
                GameManage.instance.onEnemyDied();
            Destroy(this.gameObject);
        }
    }
}
