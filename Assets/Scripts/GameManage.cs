using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour {
    public static GameManage instance = null;
    //data à conserver entre les niveaux
    public int playerLife=100;
    public int score=0;
    public int level = 1;
    public float distToInv = 1000;
    public Vector2 playerPos;
    public Vector2 playerVelocity;

    private Rigidbody2D playerRB;
    private int nbDeadEnemies=0;
    public int nbEnemies;
    private Loader loader;
    void Awake() {
        loader = GetComponent<Loader>();
        if (instance == null) {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject); //pour ne pas le detruire quand on change de scene
        }
        else {
            Destroy(gameObject);
            return;
        }
        Debug.Log(level);
        //loader.initGame(level);
    }

    

    public void onEnemyDied() {
        nbDeadEnemies++;
        Debug.Log(nbDeadEnemies+"   "+nbEnemies );
        //Debug.Log(nbDeadEnemies);
        if (nbDeadEnemies == nbEnemies) {
            nbDeadEnemies = 0;
            StartCoroutine("ReloadLevel");
        }
    }

    IEnumerator ReloadLevel() {
        yield return new WaitForSecondsRealtime(2f);//faire des trucs, fonds noir etc
        //instance.level++;
        //Debug.Log("ffff");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //This is called each time a scene is loaded.
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) {
        Debug.Log("scene loaded");
        instance.level++;
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        //loader.initGame(level);
    }

    public void UpdateScore(int scoreAdd) {
        score += scoreAdd;
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



}
