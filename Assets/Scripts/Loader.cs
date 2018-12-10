using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader:MonoBehaviour  {
    public GameObject[] Enemies;
    public GameObject[] PowerUps;
    public int[] pourcentagePop;
    public int[] cost;
    public Vector2[] rayonSpawn;

    public void initGame(int level,int nbEnemies) {

        int[] nbEnemy = new int[Enemies.Length];
        //calcul effectifs
        int sum = 0;
        for (int i = nbEnemy.Length - 1; i > 0; i--) {
            nbEnemy[i] = Mathf.RoundToInt(pourcentagePop[i] * (1 - Mathf.Exp(-level)) * nbEnemies / 100);
            sum += nbEnemy[i]*cost[i];
        }
        nbEnemy[0] = nbEnemies - sum;

        //instantiation
        for (int i = 0; i < nbEnemy.Length; i++) {
            Debug.Log("Ennemi " + i+"   "+nbEnemy[i]);
            for (int j = 0; j < nbEnemy[i]; j++) {
                //Debug.Log("Instantiate enemi " + i);
                Instantiate(Enemies[i], GameManage.instance.playerPos + randomPosition(rayonSpawn[i].x, rayonSpawn[i].y), Quaternion.identity);
            }
        }

        /*for (int i = 0; i < nbPowerUps; i++) {
            int powerUpChoisi = Random.Range(0, PowerUps.Length);
            Debug.Log(powerUpChoisi);
            Instantiate(PowerUps[powerUpChoisi], randomPosition(rayonSpawnPowerUps.x, rayonSpawnPowerUps.y), Quaternion.identity);
        }*/
    }

    private Vector2 randomPosition(float rMin, float rMax) {
        float theta = Random.Range(0, 2 * Mathf.PI);
        float r = Random.Range(rMin, rMax);
        //Debug.Log(r);
        return new Vector2(r * Mathf.Cos(theta), r * Mathf.Sin(theta));
    }

    public void SpawnPU(Vector3 position) {
        int powerUpChoisi = Random.Range(0, PowerUps.Length);
        //Debug.Log(powerUpChoisi);
        Instantiate(PowerUps[powerUpChoisi],position, Quaternion.identity);
    }

}
