using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    public Dictionary<string, int> powerUpsCorr;
    List<PowerUp> powerUps;

    private void Awake() {
        powerUpsCorr = new Dictionary<string, int>();
        powerUps = new List<PowerUp>();
    }
    public int getId(string PowerUpName) {
        return powerUpsCorr[PowerUpName];
    }
    public bool AddPowerUp(PowerUp powerUp) {
        powerUp.Assign();
        if (powerUpsCorr.ContainsKey(powerUp.powerUpName)) {
            Debug.Log("vu");
            Destroy(powerUp.gameObject);
            int id = powerUpsCorr[powerUp.powerUpName];
            powerUps[id].Reactivate();
            return false;
        }
        else {
            Debug.Log("jamais vu");
            powerUpsCorr[powerUp.powerUpName] = powerUps.Count;
            powerUps.Add(powerUp);
            return true;
        }
    }
}
