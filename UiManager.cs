using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public GameObject winPanel, startPanel, gamePanel, pausePanel, buttonNext, comingSoon, controlButtons;
    public Text timer, level, levelNameText, carrotCount;
    string levelName;

    int carrot;

    //public GameObject comingSoon, mainMenu, levelSelect;

    //private int unlockedLevel, actualLevel;


    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;

    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            levelName = "LEVEL " + (SceneManager.GetActiveScene().buildIndex).ToString();
            levelNameText.text = levelName;
        }
        carrot = 0;

        if (PlayerPrefs.GetInt("control") == 1)
        {
            controlButtons.SetActive(true);
        }
        else
        {
            controlButtons.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        carrotCount.text = carrot + "/3";


    }
    public void GameStart()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
    }


    public void GameOver()
    {
        if (SceneManager.GetActiveScene().name != "LevelEndless")
        {
            Invoke("Reload", 0.3f);
        }
        else
        {
            EndlessSpawn.instance.GameOver();
            gamePanel.SetActive(false);
        }
            
    }

    public void GameWin()
    {
        gamePanel.SetActive(false);
        winPanel.SetActive(true);
        if (Application.CanStreamedLevelBeLoaded(SceneManager.GetActiveScene().buildIndex + 1))
        {
            buttonNext.SetActive(true);
        }
        else
        {
            comingSoon.SetActive(true);
        }
        UnlockLevel();
        Time.timeScale = 0;
        //call carrot script
        //Carrot.instance.SetPlayerPref();

    }
    public void Reload()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//, LoadSceneMode.Additive);
        //SceneManager.LoadScene("CorePart", LoadSceneMode.Additive);
    }

    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        gamePanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void Unpause()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void NextLevel()
    {
        //SceneManager.LoadScene("CorePart");
        if (Application.CanStreamedLevelBeLoaded(SceneManager.GetActiveScene().buildIndex + 1))
        {
            //SceneManager.LoadScene("Level1 zaloha", LoadSceneMode.Additive);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//, LoadSceneMode.Additive);
            //SceneManager.LoadScene("CorePart", LoadSceneMode.Additive);
        }

    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }


    void UnlockLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("maxLevel"))
        {
            PlayerPrefs.SetInt("maxLevel", SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        //Debug.Log(PlayerPrefs.GetInt("maxLevel"));
    }

    public void CarrotUp()
    {
        carrot++;
    }

    public void ButtonLeft()
    {
        Ball.instance.Left();
    }
    public void ButtonRight()
    {
        Ball.instance.Right();
    }
    public void ButtonUp()
    {
        Ball.instance.Up();
    }
    public void ButtonDown()
    {
        Ball.instance.Down();
    }

}
