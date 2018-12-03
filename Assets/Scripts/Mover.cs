using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
    public float speed;
    private Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start() {
        rb.velocity = transform.up * speed;
    }
}
