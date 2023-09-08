using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public const int levelStartCount = 2;
    public const int levelNumber = 3;
    int levelUnLock;
    public Button[] button;
    public List<TMP_Text> levelMarkText;
    public List<GameObject> levelMarkView;
    public int[] levelMark; 

    private void Start() {
        Cursor.visible = true;

        levelUnLock = PlayerPrefs.GetInt( "Levels", 1);
        levelMarkText[0].text = "Mark: [" + PlayerPrefs.GetInt("levelMark1", 0).ToString() + "]";
        levelMarkText[1].text = "Mark: [" + PlayerPrefs.GetInt("levelMark2", 0).ToString() + "]";
        levelMarkText[2].text = "Mark: [" + PlayerPrefs.GetInt("levelMark3", 0).ToString() + "]";

        for ( int i = 0; i < button.Length; i++)
        {
            button[i].interactable = false;
            levelMarkView[i].SetActive(false);
        }

        Debug.Log( levelUnLock);
        for( int i = 0; i < levelUnLock; i++)
        {
            levelMarkView[i].SetActive(true);
            button[i].interactable = true;
        }
    }

    public void loadLevel( int levelIndex) {
        SceneManager.LoadScene( levelStartCount + levelIndex - 1);
    }
}

