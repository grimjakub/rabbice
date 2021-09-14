using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ball")
        {
            Invoke("FallDown", 0.3f);
        }
    }
    void FallDown()
    {
        GetComponentInParent<Rigidbody>().useGravity = true;
        GetComponentInParent<Rigidbody>().isKinematic = false;
        Destroy(transform.parent.gameObject, 2f);
    }
}
