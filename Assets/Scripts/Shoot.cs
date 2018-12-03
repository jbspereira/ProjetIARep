using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject shot;
    public Transform shotSpawn;
    public Transform shotSpawnLeft;
    public Transform shotSpawnRight;

    public bool coneShoot = false;
    public float fireRate;

    private float nextFire;


    void Update() {
        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            if (coneShoot) {
                Instantiate(shot, shotSpawnLeft.position, shotSpawnLeft.rotation);
                Instantiate(shot, shotSpawnRight.position, shotSpawnRight.rotation);
            }
        }
    }
}
