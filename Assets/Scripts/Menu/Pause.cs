using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    public GameObject PauseMenu;
    public GameObject gameOverMenu;
    private bool isPause;
    private void Awake() {
        isPause = false;
    }
    void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPause)
                Resume();
            else
                PauseFunc();
        }
	}

    public void PauseFunc() {
        Time.timeScale = 0.0f;
        isPause = true;
        PauseMenu.SetActive(true);
    }

    public void Resume() {
        Time.timeScale = 1.0f;
        isPause = false;
        PauseMenu.SetActive(false);
    }

    public void Quit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

    public void OnGameOver() {
        Time.timeScale = 0.0f;
        gameOverMenu.SetActive(true);
    }

    public void startGame() {
        SceneManager.LoadScene(1);
    }
}
