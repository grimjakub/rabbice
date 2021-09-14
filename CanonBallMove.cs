using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBallMove : MonoBehaviour
{
    public int speed;
    public float timeToDestroy=1f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
