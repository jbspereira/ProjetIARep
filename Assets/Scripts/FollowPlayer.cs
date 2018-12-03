using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public GameObject player;
	void Update () {
        Vector2 posPlayer = player.transform.position;
        transform.position = new Vector3(posPlayer.x,posPlayer.y,-10f);
	}
}
