using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour {
    public static GameManage instance = null;
    //data à conserver entre les niveaux
    public int playerLife=100;
    public int score=0;
    public int level = 1;
    [HideInInspector]
    public float distToInv = 1000;
    [HideInInspector]
    public Vector2 playerPos;
    [HideInInspector]
    public Vector2 playerVelocity;
    public float[] wavesSize;
    public float[] wavesTime;
    //public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;

    private Rigidbody2D playerRB;
    private int nbDeadEnemies=0;
    [HideInInspector]
    public int nbEnemies;
    private Loader loader;
    void Awake() {
        loader = GetComponent<Loader>();
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        if (instance == null) {
            instance = this;
            //SceneManager.sceneLoaded += OnSceneLoaded;
            //DontDestroyOnLoad(gameObject); //pour ne pas le detruire quand on change de scene
        }
        else {
            Destroy(gameObject);
            return;
        }
        StartCoroutine("NewWaveSpawn");
    }

    

    public void onEnemyDied() {
        nbDeadEnemies++;
        //Debug.Log(nbDeadEnemies+"   "+nbEnemies );
        //Debug.Log(nbDeadEnemies);
        if (nbDeadEnemies == nbEnemies) {
            nbDeadEnemies = 0;
            //StartCoroutine("ReloadLevel");
            UpdateWaveText();
            StartCoroutine("NewWaveSpawn");
        }
    }

    IEnumerator NewWaveSpawn() {
        Debug.Log("New wave coming in 10 sec");
        yield return new WaitForSecondsRealtime(5f);
        //nbEnemies = 5 * Mathf.RoundToInt(Mathf.Log(level + 20));
        nbEnemies = 6 * level + 10;
        Debug.Log("level " + level+" nbEnemies "+nbEnemies);
        int nbEnemiesLeft=nbEnemies;
        while (nbEnemiesLeft > 0) {
            int waveType = Random.Range(0, wavesSize.Length);
            float waveSize = wavesSize[waveType];
            float waveTime = wavesTime[waveType];
            int nbEnemiesWave = (int)(waveSize * nbEnemies);
            if (nbEnemiesWave > nbEnemiesLeft) {
                nbEnemiesWave = nbEnemiesLeft;
                waveTime = 0f;
            }
            nbEnemiesLeft -= nbEnemiesWave;
            Debug.Log("nbEnemies spawn : "+nbEnemiesWave+" sur "+ nbEnemies);
            loader.initGame(level, nbEnemiesWave);
            yield return new WaitForSecondsRealtime(waveTime);
        }
        instance.level++;
    }

    /*IEnumerator ReloadLevel() {
        yield return new WaitForSecondsRealtime(2f);//faire des trucs, fonds noir etc
        //instance.level++;
        //Debug.Log("ffff");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //This is called each time a scene is loaded.
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) {
        Debug.Log("scene loaded");
        //instance.level++;
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        //loader.initGame(level);
    }*/

    public void UpdateScore(int scoreAdd) {
        score += scoreAdd;
    }
    private void UpdateWaveText() {
        waveText.text = "Wave : " + level;
    }

    private void Update() {
        playerPos = playerRB.position;
        playerVelocity = playerRB.velocity;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerPos, 15f);
        foreach(Collider2D collider in colliders) {
            if (collider.tag=="PowerUp" && collider.GetComponent<PowerUp>().powerUpName == "PowerUpStar") {
                float distance = Vector2.Distance(playerPos, collider.transform.position);
                if (distance < distToInv)
                    distToInv = distance;
            }
        }
    }

    public void SpawnPU(Vector3 position) {
        loader.SpawnPU(position);
    }


}
