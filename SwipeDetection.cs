using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public Ball ball;
    private Vector3 startPos;
    public int pixelDistToDetect = 20;
    private bool fingerDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        //{
        //    startPos = Input.touches[0].position;
        //    fingerDown = true;
        //}
        //if (fingerDown)
        //{
        //    if (Input.touches[0].position.y >= startPos.y + pixelDistToDetect)
        //    {
        //        fingerDown = false;
        //        Debug.Log("Swipe up");
        //    }
        //}

        //TEST FOR PC

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
                Debug.Log("Swipe right");
            }
            if (Input.mousePosition.x < startPos.x - pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("Swipe left");
            }
            if (Input.mousePosition.y >= startPos.y + pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("Swipe up");
            }
            if (Input.mousePosition.y < startPos.y - pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("Swipe down");
            }
        }
        if(fingerDown && Input.GetMouseButtonUp(0))
        {
            fingerDown = false;
        }

    }
}
