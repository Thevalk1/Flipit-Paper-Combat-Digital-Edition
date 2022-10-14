using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject coin;
    public Rigidbody rb;

    void Start()
    {
        TossCoin();
    }

    public void TossCoin()
    {
        int jumpForce = Random.Range(50, 100);
        rb.AddForce(0, jumpForce, 10);
        int torqx = Random.Range(50, 100);
        int torqy = Random.Range(50, 100);
        rb.AddTorque(torqx, 0, torqy);
    }
}
