using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectFace : MonoBehaviour
{
    public Player player;
    public CoinBehavior coinBehavior;

    void OnTriggerStay(Collider other) {
        coinBehavior.SaveResult(player);
    }
}
