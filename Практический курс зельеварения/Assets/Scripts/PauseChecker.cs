using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseChecker : MonoBehaviour {
    public static bool gameIsPaused;
    public GameObject checker;
    public GameObject panel;

    public void Start() {
        gameIsPaused = false;
    }

    public void Update() {
        if ( gameIsPaused != Player.pauseRequired)
            ChangeState();

        if ( gameIsPaused) {
            panel.SetActive( true);
            Time.timeScale = 0f;
            Cursor.visible = true;
        }

        else {
            panel.SetActive( false);
            Time.timeScale = 1;
            Cursor.visible = false;
        }
    }

    public void ChangeState() {
        gameIsPaused = !gameIsPaused;

        if ( gameIsPaused) Debug.Log("PauseChecker : Proceed Pause");
        else Debug.Log("PauseChecker : Proceed Continue");
    }

    public void ExitToLevelScene() {
        Debug.Log("PauseChecker : Proceed Return to Menu");
        SceneManager.LoadScene(1);
    }
}
