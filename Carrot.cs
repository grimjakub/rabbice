using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    public float rotateSpeed;
    public float upDownRange;
    public float upDownSpeed;
    Vector3 pos;
    public AudioClip sound;
    //Rigidbody rb;

    // Start is called before the first frame update
    //private void Awake()
    //{
    //    rb = GetComponent<Rigidbody>();
    //}
    void Start()
    {  
        pos = transform.position;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    transform.Rotate(0, rotateSpeed, 0);
    //    //calculate what the new Y position will be
    //    float newY = Mathf.Sin(Time.time * upDownSpeed) * upDownRange + pos.y;
    //    //float newY = Mathf.PingPong(Time.time* upDownSpeed, upDownRange) + pos.y;
    //    //set the object's Y to the new calculated Y
    //    transform.position = new Vector3(pos.x, newY, pos.z);
    //}

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ball")
        {
            AudioSource.PlayClipAtPoint(sound, transform.position);
        }
    }
}
