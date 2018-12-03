using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour {

    public static GameManage instance = null;
    public GameObject[] Enemies;
    public int[] pourcentagePop;
    public Vector2[] rayonSpawn;

    public int playerLife;
    public int level = 1;

    private int nbDeadEnemies=0;
    private int nbEnemies;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject); //pour ne pas le detruire quand on change de scene
        }
        else {
            Destroy(gameObject);
            return;
        }
        Debug.Log(level);
        
        initGame();
    }

    private void initGame() {
        nbEnemies = 5*Mathf.RoundToInt(Mathf.Log(level+1));
        int[] nbEnemy = new int[Enemies.Length];
        //calcul effectifs
        int sum = 0;
        for (int i = nbEnemy.Length-1; i > 0; i--) {
            nbEnemy[i]= Mathf.RoundToInt(pourcentagePop[i] * (1 - Mathf.Exp(-level)) * nbEnemies / 100);
            sum += nbEnemy[i];
        }
        nbEnemy[0] = nbEnemies - sum;

        //instantiation
        for (int i = 0; i < nbEnemy.Length; i++) {
            //Debug.Log("Enemi " + i+"   "+nbEnemy[i]);
            for (int j = 0; j < nbEnemy[i]; j++) {
                //Debug.Log("Instantiate enemi " + i);
                Instantiate(Enemies[i], randomPosition(rayonSpawn[i].x, rayonSpawn[i].y),Quaternion.identity);
            }
        }
    }

    private Vector2 randomPosition(float rMin,float rMax) {
        float theta = Random.Range(0,2*Mathf.PI);
        float r = Random.Range(rMin, rMax);
        Debug.Log(r);
        return new Vector2(r*Mathf.Cos(theta),r*Mathf.Sin(theta));
    }

    public void onEnemyDied() {
        nbDeadEnemies++;
        //Debug.Log(nbDeadEnemies);
        if (nbDeadEnemies == nbEnemies) {
            nbDeadEnemies = 0;
            StartCoroutine("ReloadLevel");
        }
    }

    IEnumerator ReloadLevel() {
        yield return new WaitForSecondsRealtime(2f);
        //instance.level++;
        //Debug.Log("ffff");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static public void CallbackInitialization() {
        //register the callback to be called everytime the scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //This is called each time a scene is loaded.
    static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) {
        Debug.Log("ffff");
        instance.level++;
        instance.initGame();
    }





}
