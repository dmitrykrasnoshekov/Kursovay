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

        if ( currentLevel >= PlayerPrefs.GetInt("Levels", 1) && currentLevel < LevelManager.levelNumber) {
            int mark = 0;
            switch (currentLevel)
            {
                case 1:
                mark = PlayerPrefs.GetInt("levelMark1");
                break;

                case 2:
                mark = PlayerPrefs.GetInt("levelMark2");
                break;

                case 3:
                mark = PlayerPrefs.GetInt("levelMark3");
                break;
            }

            if (mark > 2)
            {
                Debug.Log("NextLevel");
                Debug.Log(currentLevel + 2);
                PlayerPrefs.SetInt("Levels", currentLevel + 1);
            }
        }
    }
}
