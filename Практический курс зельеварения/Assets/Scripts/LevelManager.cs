using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public const int levelStartCount = 3;
    public const int levelNumber = 3;
    int levelUnLock;
    public Button[] buttons;

    void Start() {
        // Cursor.visible = true;

        levelUnLock = PlayerPrefs.GetInt( "Levels", 1);

        for( int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for( int i = 0; i < levelUnLock; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void loadLevel( int levelIndex) {
        SceneManager.LoadScene( levelStartCount + levelIndex - 1);
    }
}

