using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DestroyPlayer : MonoBehaviour {

    public int damageOnCollide = 1;
    public int scoreGain = 1;
    public int probaDrop=20;
    public GameObject explosion;

    private static Animator CameraShake;
    private bool contact=false;

    private void Start() {
        if (CameraShake==null)
            CameraShake = Camera.main.GetComponent<Animator>();
    }
    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<LifeComponent>().changeLife(damageOnCollide);
            contact = true;
        }
        else if (collision.gameObject.tag == "bullet") {
            Destroy(collision.gameObject);
            contact = true;
        }
        else if (collision.gameObject.tag == "bomb") {
            contact = true;
        }
    }

    private void Update() { //on fait ça dans le update sinon les messages sont parfois envoyés deux fois dans le cas où un tir et le joueur touchent un ennemi durant la meme frame
        if (contact) {
            if (GameManage.instance != null) {
                int spawnPU = Random.Range(0, 100);
                if (spawnPU < probaDrop) {
                    GameManage.instance.SpawnPU(transform.position);
                }
                GameManage.instance.onEnemyDied();
                GameManage.instance.UpdateScore(scoreGain);
            }
            CameraShake.SetTrigger("Shake");
            Instantiate(explosion,transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
