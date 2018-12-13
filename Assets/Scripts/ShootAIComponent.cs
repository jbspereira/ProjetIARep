using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAIComponent : MonoBehaviour {

    public GameObject shot;
    public Transform shotSpawn;
    public bool canShoot = false;
    public float fireRate=0.2f;
    private float nextFire;
    private void Awake() {
        nextFire = Time.time;
    }
    void Update () {
		if (canShoot && nextFire<Time.time) {
            nextFire = Time.time+fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
	}
}
