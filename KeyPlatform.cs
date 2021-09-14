using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPlatform : MonoBehaviour
{
    public static KeyPlatform instance;
    public GameObject winPlatform;
    public GameObject key;
    Vector3 pos,pos2;
    //public float speed=5;

    public GameObject close, open;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //key.GetComponent<Renderer>().material = close;
        pos = winPlatform.transform.position;
        winPlatform.transform.position = new Vector3(pos.x, 0, pos.z);
        //pos2 = new Vector3(pos.x, 0, pos.z);
        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
        //float step = speed * Time.deltaTime;
        //winPlatform.transform.position = Vector3.MoveTowards(pos2,pos, step);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ball")
        {
            Debug.Log("now");
            KeyPress();
        }
    }

    public void KeyPress()
    {
        Debug.Log("now2");
        winPlatform.transform.position = new Vector3(pos.x, pos.y, pos.z);
        //key.GetComponent<Renderer>().material = open;
        close.SetActive(false);
        open.SetActive(true);
    }
}
