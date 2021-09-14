using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float rangeX=1;
    public float rangeZ = 0;
    public float speed;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //calculate what the new Y position will be
        float newX = Mathf.Sin(Time.time * speed) * rangeX + pos.x;
        float newZ = Mathf.Sin(Time.time * speed) * rangeZ + pos.z;
        //float newX = Mathf.PingPong(Time.time*speed,range) + pos.x;
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(newX, pos.y, newZ);
    }
}
