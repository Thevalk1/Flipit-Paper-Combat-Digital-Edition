using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationsPerMinute = 10.0f;

    void Update()
    {
        transform.Rotate(0.0f, 6.0f * rotationsPerMinute * Time.deltaTime, 0.0f);
    }
}
