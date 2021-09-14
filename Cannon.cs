using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float delayTimer;
    float timer;
    Vector3 pos;
    public GameObject SpawnPlace;
    public GameObject cannonBall;
    // Start is called before the first frame update
    void Start()
    {
        timer = delayTimer;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnBall();
    }

    void SpawnBall()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            pos = SpawnPlace.transform.position;
            Instantiate(cannonBall, pos, transform.rotation);
            timer = delayTimer;
            //Invoke("DestroyBall", timeToDestroy);
            
        }
    }

    void DestroyBall()
    {

    }

    void MoveBall()
    {

    }
}
