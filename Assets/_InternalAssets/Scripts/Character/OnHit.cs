using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHit : MonoBehaviour
{
    public Animator animator;
    public Faction faction;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        animator.SetBool("Dead", true);
        GameManager.Instance.KillSoldier(faction);
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
