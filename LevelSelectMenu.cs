using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour
{
    private int totalLevel;
    private int unlockedLevel=1;
    private LevelButton[] levelButtons;
    private int totalPage = 0;
    private int page = 0;
    private int pageItem = 9;
    int totalStars;

    public GameObject nextButton, backButton;

    void OnEnable()
    {
        levelButtons = GetComponentsInChildren<LevelButton>();
    }

    void Awake()
    {
        SetScore();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        //Refresh();
        if (PlayerPrefs.HasKey("maxLevel"))
        {
            //Debug.Log(PlayerPrefs.GetInt("maxLevel")+"a");
            unlockedLevel = PlayerPrefs.GetInt("maxLevel");
            //Debug.Log(unlockedLevel + "lvl");
        }
        else
        {
            //Debug.Log(PlayerPrefs.GetInt("maxLevel") + "b");
        }
        
        
        totalLevel = SceneManager.sceneCountInBuildSettings - 2;
        
        Refresh();
        //TotalStars();
    }

    public void StartLevel(int level)
    {
        //SceneManager.LoadScene("CorePart");
        SceneManager.LoadScene(level);//, LoadSceneMode.Additive);
        //if (level == unlockedLevel)
        //{
        //    unlockedLevel++;
        //    Refresh();
        //}

        //int star = GetStar(level);
        //star = Mathf.Clamp(star + 1, 0, 3);
        //SetStar(level, star);
    }

    public void CliclNext()
    {
        page += 1;
        Refresh();
    }

    public void ClickBack()
    {
        page -= 1;
        Refresh();
    }

    public void Refresh()
    {
        totalPage = totalLevel / pageItem;
        int index = page * pageItem;
        for(int i = 0; i < levelButtons.Length; i++)
        {
            int level = index + i + 1;

            if (level <= totalLevel)
            {
                levelButtons[i].gameObject.SetActive(true);
                levelButtons[i].Setup(level,GetStar(level), level <= unlockedLevel);
                totalStars += GetStar(level);
                //if (PlayerPrefs.GetInt("TotalStars") < totalStars)
                //{
                //    PlayerPrefs.SetInt("TotalStars", totalStars);
                //}
            }
            else
            {
                levelButtons[i].gameObject.SetActive(false);
            }
        }
        CheckButton();
        TotalStars();

    }
    private void CheckButton()
    {
        backButton.SetActive(page > 0);
        nextButton.SetActive(page < totalPage);
    }

    private void SetStar(int level, int starAmount)
    {
        PlayerPrefs.SetInt(GetKey(level), starAmount);
    }
    private int GetStar(int level)
    {
        return PlayerPrefs.GetInt(GetKey(level));
    }
    private string GetKey(int level)
    {
        // Level_3_Star
        return "Level_" + level + "_Star";
    }

    void TotalStars()
    {
        PlayerPrefs.SetInt("totalStars", totalStars);

        //PlayerPrefs.SetInt("TotalStars", 0);
        //for (int i = 0; i < PlayerPrefs.GetInt("maxLevel"); i++)
        //{
        //    totalStars += GetStar(i);
        //    Debug.Log(i+"iii"+totalStars) ;
        //}
        //PlayerPrefs.SetInt("TotalStars", totalStars);
        //if (PlayerPrefs.GetInt("TotalStars") < totalStars)
        //{
        //    PlayerPrefs.SetInt("TotalStars", totalStars);
        //}

    }

    void SetScore()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("maxLevel"); i++)
        {
            totalStars += GetStar(i);
            //Debug.Log(i + "iii" + totalStars);
        }
        PlayerPrefs.SetInt("testStars", totalStars);
    }
}
