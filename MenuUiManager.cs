using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUiManager : MonoBehaviour
{
    public GameObject comingSoon, mainMenu, levelSelect,setting, credits;
    public Text LevelCompelted, stars,audiotext,controlerText;

    // Start is called before the first frame update

    void Start()
    {
        LevelCompelted.text = "FINISHED " + (PlayerPrefs.GetInt("maxLevel")-1) + "/" + (SceneManager.sceneCountInBuildSettings-2);
        stars.text = "COLLECTED: " + PlayerPrefs.GetInt("testStars");
        if (PlayerPrefs.GetInt("muted") == 1)
        {
            AudioListener.pause = true;
            audiotext.text = "SOUNDS OFF";
        }
        else
        {
            AudioListener.pause = false;
            audiotext.text = "SOUNDS ON";
        }

        if (PlayerPrefs.GetInt("control") == 0)
        {
            controlerText.text = "SWIPE";
        }
        else
        {
            controlerText.text = "ARROW KEYS";
        }
    }

    // Update is called once per frame


    public void Back()
    {
        SceneManager.LoadScene(0);
    }

    public void ComingSoon()
    {
        mainMenu.SetActive(false);
        comingSoon.SetActive(true);
    }

    public void LevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
    }
    public void Continue()
    {
        if (PlayerPrefs.GetInt("maxLevel") < 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        //Debug.Log(PlayerPrefs.GetInt("maxLevel"));
        //SceneManager.LoadScene("CorePart");
        else if (Application.CanStreamedLevelBeLoaded(PlayerPrefs.GetInt("maxLevel")))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("maxLevel"));
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//, LoadSceneMode.Additive);
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("CorePart", LoadSceneMode.Additive);
    }

    public void LoadMode()
    {
        SceneManager.LoadScene("LevelEndless");
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("**** PlayerPref deleted ****");
    }

    public void UnlockAll()
    {
        PlayerPrefs.SetInt("maxLevel", SceneManager.sceneCountInBuildSettings-2);
    }

    public void Setting()
    {
        mainMenu.SetActive(false);
        setting.SetActive(true);
    }

    public void Credits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }

    public void AudioSetting()
    {
        if (PlayerPrefs.GetInt("muted") == 0)
        {
            PlayerPrefs.SetInt("muted", 1);
            AudioListener.pause = true;
            audiotext.text = "SOUNDS OFF";
        }
        else
        {
            PlayerPrefs.SetInt("muted", 0);
            AudioListener.pause = false;
            audiotext.text = "SOUNDS ON";
        }
    }

    // 0=SWIPE, 1=ARROW 
    public void ControlSetting()
    {
        if (PlayerPrefs.GetInt("control") == 1)
        {
            PlayerPrefs.SetInt("control", 0);
            controlerText.text = "SWIPE";
        }
        else
        {
            PlayerPrefs.SetInt("control", 1);
            controlerText.text = "ARROW KEYS";
        }
    }

    //public void SetQuality(int qualityIndex)
    //{
    //    QualitySettings.SetQualityLevel(qualityIndex);
    //}
}
