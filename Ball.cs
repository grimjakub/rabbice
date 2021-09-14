using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public static Ball instance;
    public GameObject particle;
    Rigidbody rb;
    public float speed;
    public bool started;
    public bool gameOver, gameWin;
    private Vector3 startPos;
    public int pixelDistToDetect = 20;
    private bool fingerDown;
    int carrots;
    public AudioClip winSound;
    public Animator anim;
    //Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        started = false;
        gameOver = false;
        gameWin = false;
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        carrots = 0;
        //anim.Play("Jumping");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && !gameWin)
        {
            ChangeDirection();
        }

        if (!started)
        {
            if (Input.GetMouseButtonDown(0) | Input.anyKeyDown)
            {
                rb.velocity = new Vector3( 0, 0, speed);
                started = true;
                UiManager.instance.GameStart();
                anim.Play("Running");
            }
        }
        //zajistim pohyb po nárazu do koule
        if (!gameOver && !gameWin && started)
        {
            if (rb.velocity.magnitude < speed / 2)
            {
                rb.velocity = rb.transform.forward * speed;
            }
        }

        //ovìøit zda jsem na platformì
        if (!Physics.Raycast(transform.position, Vector3.down, 1f))
        {
            gameOver = true;
            //anim.Play("Fall");
            rb.velocity = new Vector3(0, -5f, 0)+rb.velocity;
            Camera.main.GetComponent<CameraFollow>().gameOver = true;
            UiManager.instance.GameOver();

            if (SceneManager.GetActiveScene().name == "LevelEndless")
            {
                //Debug.Log("LevelEndless GameOver");
                EndlessSpawn.instance.GameOver();
            }
            
        }


    }


    void ChangeDirection()
    {
        if (PlayerPrefs.GetInt("control") == 0)
        {
            Swipe();
        }
            
        ChangeDirKeyboard();
    }

    void Swipe()
    {
        if (fingerDown == false && Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            fingerDown = true;
        }
        if (fingerDown)
        {
            if (Input.mousePosition.x >= startPos.x + pixelDistToDetect)
            {
                fingerDown = false;
                //Debug.Log("Swipe right");
                rb.velocity = new Vector3(speed, 0, 0);
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            if (Input.mousePosition.x < startPos.x - pixelDistToDetect)
            {
                fingerDown = false;
                //Debug.Log("Swipe left");
                rb.velocity = new Vector3(-speed, 0, 0);
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            if (Input.mousePosition.y >= startPos.y + pixelDistToDetect)
            {
                fingerDown = false;
                //Debug.Log("Swipe up");
                rb.velocity = new Vector3(0, 0, speed);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (Input.mousePosition.y < startPos.y - pixelDistToDetect)
            {
                fingerDown = false;
                //Debug.Log("Swipe down");
                rb.velocity = new Vector3(0, 0, -speed);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        if (fingerDown && Input.GetMouseButtonUp(0))
        {
            fingerDown = false;
        }
    }
    public void Up()
    {
        rb.velocity = new Vector3(0, 0, speed);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void Down()
    {
        rb.velocity = new Vector3(0, 0, -speed);
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    public void Left()
    {
        rb.velocity = new Vector3(-speed, 0, 0);
        transform.rotation = Quaternion.Euler(0, -90, 0);
    }
    public void Right()
    {
        rb.velocity = new Vector3(speed, 0, 0);
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }
    void ChangeDirKeyboard()
    {
        //----ovladani šipkama just for fun------
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Left();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Right();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Up();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Down();
        }
    }

    void SetCollectedCarrots()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        string lvlName = "Level_" + level + "_Star";
        if (carrots > PlayerPrefs.GetInt(lvlName))
        {
            PlayerPrefs.SetInt(lvlName, carrots);
        }
        //Debug.Log(PlayerPrefs.GetInt(lvlName));
    }

    //efekty pøi kolizi
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "WinPlatform")
        {
            AudioSource.PlayClipAtPoint(winSound, transform.position);
            gameWin = true;
            rb.velocity = new Vector3(0, 0, 0);
            SetCollectedCarrots();
            UiManager.instance.GameWin();
            //anim.SetTrigger("mixamo_com 1");
            anim.Play("Jumping");
        }

        else if (col.gameObject.tag == "SpeedUp")
        {
            speed = speed * 2;
            rb.velocity = rb.velocity * 2;
            //Debug.Log("SpeedUp");
        }

        else if (col.gameObject.tag == "SpeedDown")
        {
            speed = speed / 2;
            rb.velocity = rb.velocity/2;
            //Debug.Log("SpeedDown");
        }
        else if (col.gameObject.tag == "Carrot")
        {
            GameObject part=Instantiate(particle, col.gameObject.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
            Destroy(col.gameObject);
            carrots++;
            UiManager.instance.CarrotUp();
            Destroy(part, 1f);
        }

        else if (col.gameObject.tag == "CarrotEndless")
        {
            GameObject part = Instantiate(particle, col.gameObject.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
            Destroy(col.gameObject);
            carrots++;
            EndlessSpawn.instance.CarrotUpEndless();
            Destroy(part, 1f);
        }
        //else if (col.gameObject.tag == "KeyPlatform")
        //{
        //    Debug.Log("KeyPlatform");
        //    KeyPlatform.instance.KeyPress();
        //}
    }

    ////test lepšího ovládání..NEFUNGUJE!!
    //void ChangeDirection2()
    //{
    //    if (fingerDown == false && Input.GetMouseButtonDown(0))
    //    {
    //        startPos = Input.mousePosition;
    //        fingerDown = true;
    //        Debug.Log("mouse_01");
    //    }
    //    if (fingerDown)
    //    {
    //        Debug.Log("mouse_02");
            
    //        if (Input.mousePosition.x >= startPos.x + pixelDistToDetect)
    //        {
    //            //fingerDown = false;
    //            Debug.Log("Swipe right");
    //            rb.velocity = new Vector3(speed, 0, 0);
    //            transform.rotation = Quaternion.Euler(0, 90, 0);
    //        }
    //        if (Input.mousePosition.x < startPos.x - pixelDistToDetect)
    //        {
    //            //fingerDown = false;
    //            Debug.Log("Swipe left");
    //            rb.velocity = new Vector3(-speed, 0, 0);
    //            transform.rotation = Quaternion.Euler(0, -90, 0);
    //        }
    //        if (Input.mousePosition.y >= startPos.y + pixelDistToDetect)
    //        {
    //            //fingerDown = false;
    //            Debug.Log("Swipe up");
    //            rb.velocity = new Vector3(0, 0, speed);
    //            transform.rotation = Quaternion.Euler(0, 0, 0);
    //        }
    //        if (Input.mousePosition.y < startPos.y - pixelDistToDetect)
    //        {
    //            //fingerDown = false;
    //            Debug.Log("Swipe down");
    //            rb.velocity = new Vector3(0, 0, -speed);
    //            transform.rotation = Quaternion.Euler(0, 180, 0);
    //        }
    //    }
    //    if (fingerDown && Input.GetMouseButtonUp(0))
    //    {
    //        fingerDown = false;
    //    }
    //}
}
