using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinBehavior : MonoBehaviour
{
    public GameObject coin;
    public Rigidbody rb;

    private float speed;
    private bool tossedCoin = false;

    void Start() {
        TossCoin();
    }

    void Update () {
        if (tossedCoin) {
            StartCoroutine(GetResult());
        }
    }

    public void TossCoin()
    {
        tossedCoin = true;

        int jumpForce = Random.Range(50, 100);
        rb.AddForce(0, jumpForce, 10);
        int torqx = Random.Range(50, 100);
        int torqy = Random.Range(50, 100);
        rb.AddTorque(torqx, 0, torqy);
    }

    private IEnumerator GetResult()
    {
        while (!Mathf.Approximately(rb.velocity.magnitude, 0))
        {
            yield return null;
        }

        rb.velocity = Vector3.zero;

        tossedCoin = false;

        Debug.Log("Need to get face up value now");

        // TODO: Proceed to movement phase after getting winner in coin toss
    }
}
