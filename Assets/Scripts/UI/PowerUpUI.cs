using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PowerUpUI : MonoBehaviour {

    Dictionary<string, int> powerUpToImage;
    bool[] slotTaken;

    private void Awake() {
        powerUpToImage = new Dictionary<string, int>();
        slotTaken = new bool[gameObject.transform.childCount];
    }
    public void AssignSlotToPowerUp(string numPowerUp,Color color) {
        //Debug.Log(numPowerUp);
        if (powerUpToImage.ContainsKey(numPowerUp)) {
            gameObject.transform.GetChild(powerUpToImage[numPowerUp]).gameObject.SetActive(true);
            return;
        }
        for (int i = 0; i < slotTaken.Length;i++) {
            GameObject slot = gameObject.transform.GetChild(i).gameObject;
            if (!slotTaken[i] && !slot.activeInHierarchy) {
                slotTaken[i] = true;
                powerUpToImage[numPowerUp] = i;
                Image slotImage = slot.GetComponent<Image>();
                slot.SetActive(true);
                slotImage.color = color;
                break;
            }
        }
    }

    public void UpdateSlot(string numPowerUp,float percentage) {
        GameObject slot = gameObject.transform.GetChild(powerUpToImage[numPowerUp]).gameObject;
        Image slotImage = slot.GetComponent<Image>();
        slotImage.fillAmount = percentage;
    }

    public void PowerUpFinished(string numPowerUp) {
        gameObject.transform.GetChild(powerUpToImage[numPowerUp]).gameObject.SetActive(false);
    }

}
