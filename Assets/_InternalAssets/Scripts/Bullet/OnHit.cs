using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHit : MonoBehaviour
{
    public float TimeToLive = 3f;

    private void Start()
    {
        Destroy(gameObject, TimeToLive);
    }

    void onTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Soldier")other.gameObject.tag
        {
            Debug.Log("Hit");
        }

        Destroy(this);
    }
}
