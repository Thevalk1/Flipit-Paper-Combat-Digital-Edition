using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    public Faction faction;

    void OnMouseOver()
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
            || (
                GameManager.Instance.State == GameState.Player1ShootingTurn
                && GameManager.Instance.player1Faction == faction
            )
            || (
                GameManager.Instance.State == GameState.Player2ShootingTurn
                && GameManager.Instance.player2Faction == faction
            )
        )
        {
            if (GetComponent<Outline>() == null)
            {
                var outline = gameObject.AddComponent<Outline>();

                outline.OutlineMode = Outline.Mode.OutlineAll;
                outline.OutlineColor = Color.yellow;
                outline.OutlineWidth = 5f;
            }
        }

        if (Input.GetMouseButtonDown(1))
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
                GameManager.Instance.StartCharacterShooting(gameObject);
            }
        }
    }

    void OnMouseExit()
    {
        if (GetComponent<Outline>())
        {
            Destroy(GetComponent<Outline>());
        }
    }
}
