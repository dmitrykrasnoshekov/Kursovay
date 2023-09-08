using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartConfigScript : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        Cursor.visible = true;
        PlayerPrefs.SetInt( "Levels", 1);

        PlayerPrefs.SetInt("levelMark1", 0);
        PlayerPrefs.SetInt("levelMark2", 0);
        PlayerPrefs.SetInt("levelMark3", 0);
    }

    public void loadLevelScene() {
        SceneManager.LoadScene( 1);
    }

    public void Exit() {
        Application.Quit();
    }
}
