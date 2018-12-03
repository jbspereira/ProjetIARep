using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpListeners : MonoBehaviour {

    //design pattern singleton
    public static PowerUpListeners instance;
    public List<GameObject> listeners;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
        listeners = new List<GameObject>();
    }

    public void RegisterAsListener(GameObject listener) {
        listeners.Add(listener);
    }
}
