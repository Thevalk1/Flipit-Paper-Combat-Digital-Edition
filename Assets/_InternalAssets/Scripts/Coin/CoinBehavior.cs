using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinBehavior : MonoBehaviour
{
    public GameObject coin;
    public Rigidbody rb;
    public TextMeshProUGUI resultText;

    private float speed;
    private bool tossedCoin = false;
    private Player result;

    void Start()
    {
        TossCoin();
    }

    void Update()
    {
        if (tossedCoin)
        {
            StartCoroutine(GetResult());
        }
    }

    public void TossCoin()
    {
        resultText.text = "";
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

        DisplayResult();
    }

    public void SaveResult(Player player)
    {
        result = player;
    }

    private void DisplayResult()
    {
        switch (result)
        {
            case Player.Player1:
                resultText.text = "Player 1 Goes First!";
                break;
            case Player.Player2:
                resultText.text = "Player 2 Goes First!";
                break;
        }

        
    }
}
