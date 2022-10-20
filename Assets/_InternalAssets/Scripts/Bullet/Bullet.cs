using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float TimeToLive = 3f;

    private void Start()
    {
        Destroy(gameObject, TimeToLive);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
