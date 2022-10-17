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
    public Player result;

    public void TossCoin()
    {
        if (tossedCoin == false)
        {
            tossedCoin = true;
            resultText.text = "";

            int jumpForce = Random.Range(70, 100);
            rb.AddForce(0, jumpForce, 10);
            int torqx = Random.Range(70, 100);
            int torqy = Random.Range(70, 100);
            rb.AddTorque(torqx, 0, torqy);
        }
    }

    private IEnumerator GetResult()
    {
        while (!Mathf.Approximately(rb.velocity.magnitude, 0))
        {
            yield return null;
        }

        rb.velocity = Vector3.zero;

        tossedCoin = false;
        StartCoroutine(DisplayResult());
    }

    public void SaveResult(Player player)
    {
        result = player;

        StartCoroutine(GetResult());
    }

    private IEnumerator DisplayResult()
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

        yield return new WaitForSeconds(2);

        resultText.text = "";

        yield return new WaitForSeconds(1);

        GameManager.Instance.StartNewTurn(result);
    }
}
