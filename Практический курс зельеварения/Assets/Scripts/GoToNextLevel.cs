using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextLevel : MonoBehaviour {
    private void OnTriggerEnter( Collider collision) {
        Debug.Log( "Trigger Door");
        Debug.Log( collision.tag);
        if ( collision.CompareTag("Player")) {
            UnLockLevel();
            SceneManager.LoadScene(1);
        }
    }
    public void UnLockLevel() {
        int currentLevel = SceneManager.GetActiveScene().buildIndex - LevelManager.levelStartCount + 1;
        Debug.Log( "Finish Level");
        Debug.Log( currentLevel + 1);

        if ( currentLevel >= PlayerPrefs.GetInt("Levels", 1) && currentLevel < LevelManager.levelNumber ) {
            Debug.Log( "NextLevel");
            Debug.Log(currentLevel + 2);
            PlayerPrefs.SetInt( "Levels", currentLevel + 1);
        }
    }
}
