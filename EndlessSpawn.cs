using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndlessSpawn : MonoBehaviour
{
    public static EndlessSpawn instance;
    public Ball Ball;
    public GameObject platform, carrot,start, gameOverPanel;
    bool gameOver;
    Vector3 lastPos;
    float size;
    int score;
    float timer;
    public GameObject player;
    public Text carrotCount,bestScore,timerText,bestTime,yourScore,yourTime;
    public int distanceToSpawn=20;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;

    }



    void Start()
    {
        gameOver = Ball.gameOver;
        lastPos = start.transform.position;
        size=platform.transform.localScale.x;
        BestScore();
        for (int i = 0; i < 30; i++)
        {
            Spawn();

        }
        //StartSpawning();  
    }
    public void StartSpawning()
    {
        InvokeRepeating("Spawn", 0.05f, 0.05f);
        //InvokeRepeating("TimeCount", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //if (gameOver)
        //{
        //    CancelInvoke("Spawn");
        //    //SetMaxScore();
            
        //}
        if (!gameOver & Ball.started)
        {
            timer += Time.deltaTime;
            Spawn();
        }
        carrotCount.text = score+"";
        timerText.text = "TIME: " + timer.ToString("F2")+"s";
    }



    //random spawn a pak funkce na jednotlive smery
    void Spawn()
    {
        if (gameOver)
        {
            return;
        }
        int rand = Random.Range(0, 6);
        if (Vector3.Distance(player.transform.position, lastPos)< distanceToSpawn)
        {
            if (rand < 2)
            {
                SpawnLeft();
            }
            else if (rand < 4 & rand > 2)
            {
                SpawnRight();
            }
            else if (rand >= 4)
            {
                SpawnForward();
            }
        }

    }

    void SpawnLeft()
    {
        Vector3 pos = lastPos;
        pos.x -= size;
        lastPos = pos;
        if (Physics.CheckSphere(pos, 0.5f))
        {
            Instantiate(platform, pos, Quaternion.identity);
        }
        

        int rand = Random.Range(0, 5);
        if (rand < 1)
        {
            Instantiate(carrot, new Vector3(pos.x, pos.y + 1.45f, pos.z), carrot.transform.rotation);
        }
    }

    void SpawnRight()
    {
        Vector3 pos = lastPos;
        pos.x += size;
        lastPos = pos;
        if (Physics.CheckSphere(pos, 0.5f))
        {
            Instantiate(platform, pos, Quaternion.identity);
        }

        int rand = Random.Range(0, 5);
        if (rand < 1)
        {
            Instantiate(carrot, new Vector3(pos.x, pos.y + 1.45f, pos.z), carrot.transform.rotation);
        }
    }

    void SpawnForward()
    {
        Vector3 pos = lastPos;
        pos.z += size;
        lastPos = pos;
        if (Physics.CheckSphere(pos, 0.5f))
        {
            Instantiate(platform, pos, Quaternion.identity);
        }

        int rand = Random.Range(0, 5);
        if (rand < 1)
        {
            Instantiate(carrot, new Vector3(pos.x, pos.y + 1.45f, pos.z), carrot.transform.rotation);
        }
    }


    public void CarrotUpEndless()
    {
        score++;
    }

    void SetMaxScore()
    {
        //Debug.Log("set score");
        if(PlayerPrefs.GetInt("maxScoreEndless") < score)
        {
            PlayerPrefs.SetInt("maxScoreEndless", score);
        }

        if (PlayerPrefs.GetFloat("maxTimeEndless") < timer)
        {
            PlayerPrefs.SetFloat("maxTimeEndless", timer);
        }

    }

    void BestScore()
    {
        bestScore.text ="BEST CARROTS:"+ PlayerPrefs.GetInt("maxScoreEndless");
        bestTime.text="BEST TIME: "+ PlayerPrefs.GetFloat("maxTimeEndless").ToString("F2") + "s";
    }

    void TimeCount()
    {
        timer++;
    }

    public void GameOver()
    {
        gameOver = true;
        //Time.timeScale = 0;
        yourScore.text= "COLLECTED CARROTS:" + score;
        yourTime.text = "YOUR TIME:" + timer.ToString("F2") + "s";
        SetMaxScore();
        Invoke("ShowPanel", 0.3f);
        
    }

    void ShowPanel()
    {
        gameOverPanel.SetActive(true);
    }

}
