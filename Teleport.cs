using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject spawnPlace;
    //public GameObject particle;
    //public GameObject rotPos;
    Vector3 pos;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ball")
        {
            //GameObject part = Instantiate(particle, col.gameObject.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
            //pos = col.transform.position;
            col.transform.position = spawnPlace.transform.position;
            col.transform.rotation = spawnPlace.transform.rotation;
            rb = col.GetComponent<Rigidbody>();
            rb.velocity = spawnPlace.transform.forward * rb.velocity.magnitude;
            //Destroy(part, 1f);

           



        }
    }
}
