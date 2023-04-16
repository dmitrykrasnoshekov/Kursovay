using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartConfigScript : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        PlayerPrefs.SetInt( "Levels", 1);
    }

    // Update is called once per frame
    public void loadLevelScene() {
        SceneManager.LoadScene( 1);
    }
}
