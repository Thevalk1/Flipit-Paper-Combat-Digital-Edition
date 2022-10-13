using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseFaction : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("clicked");
        Destroy(this.gameObject);
    }
}
