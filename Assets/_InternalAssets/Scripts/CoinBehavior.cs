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
        cointoss();
    }
    public void cointoss()
    {   
        int jumpForce = Random.Range(20,100);
        rb.AddForce(0,jumpForce,10);
        int torqx = Random.Range(20,100);
        int torqy = Random.Range(20,100);    
        rb.AddTorque(torqx,0,torqy);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
