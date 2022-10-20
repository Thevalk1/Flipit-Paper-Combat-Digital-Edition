using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    public Faction faction;

    void OnMouseDown()
    {
        if (
            (
                GameManager.Instance.State == GameState.Player1MovementTurn
                && GameManager.Instance.player1Faction == faction
            )
            || (
                GameManager.Instance.State == GameState.Player2MovementTurn
                && GameManager.Instance.player2Faction == faction
            )
        )
        {
            GameManager.Instance.StartCharacterMovement(gameObject);
        }
        else if (
            (
                GameManager.Instance.State == GameState.Player1ShootingTurn
                && GameManager.Instance.player1Faction == faction
            )
            || (
                GameManager.Instance.State == GameState.Player2ShootingTurn
                && GameManager.Instance.player2Faction == faction
            )
        )
        {
            // TODO: Shooting character select
        }
    }
}
