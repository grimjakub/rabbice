using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public LevelSelectMenu menu;
    public Sprite lockSprite;
    public Sprite buttonSprite;
    public Text levelText;
    public GameObject levelStarPrefab;

    private int level = 0;
    private Button button;
    private Image image;
    private LevelStar levelStar;
    
    void OnEnable()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        levelStar = Instantiate(levelStarPrefab, gameObject.transform).GetComponent<LevelStar>();
    }

    public void Setup(int level, int star, bool isUnlock)
    {
        this.level = level;
        levelText.text = level.ToString();
        if (isUnlock)
        {
            image.sprite = buttonSprite;
            button.enabled = true;
            levelStar.gameObject.SetActive(true);
            levelStar.SetStarSprite(star);
            levelText.gameObject.SetActive(true);

            


        }
        else
        {
            image.sprite = lockSprite;
            button.enabled = false;
            levelText.gameObject.SetActive(false);
            levelStar.gameObject.SetActive(false);
        }
    }
    public void OnClick()
    {
        menu.StartLevel(level);
    }
}
