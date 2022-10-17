using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCamera : MonoBehaviour
{
    public GameObject coin;
    public float xOffset,
        yOffset,
        zOffset;

    void Update()
    {
        transform.position = coin.transform.position + new Vector3(xOffset, yOffset, zOffset);
        transform.LookAt(coin.transform.position);
    }
}
