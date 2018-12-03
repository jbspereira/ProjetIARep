using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideBehaviour : MonoBehaviour {

    public bool invicibility = false;
    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(collision.gameObject.tag);
        if (!invicibility)
            GetComponent<LifeComponent>().changeLife(collision.gameObject.GetComponent<DestroyPlayer>().damageOnCollide);
        Destroy(collision.gameObject);
    }
}
