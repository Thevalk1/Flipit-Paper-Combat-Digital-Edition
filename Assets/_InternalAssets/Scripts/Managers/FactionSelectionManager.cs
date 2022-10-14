using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FactionSelectionManager : MonoBehaviour
{
    public static FactionSelectionManager Instance;

    [SerializeField]
    private GameObject _factionSelectPanel;

    [SerializeField]
    private GameObject _player1Text;

    [SerializeField]
    private GameObject _player2Text;

    [SerializeField]
    private GameObject _britishSelection;

    [SerializeField]
    private GameObject _germanSelection;

    void Awake()
    {
        Instance = this;

        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
        _factionSelectPanel.SetActive(false);
    }

    void onDestroy()
    {
        GameManager.onGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        _factionSelectPanel.SetActive(
            state == GameState.Player1SelectArmy || state == GameState.Player2SelectArmy
        );

        switch (state)
        {
            case GameState.Player1SelectArmy:
                _player2Text.SetActive(false);
                _player1Text.SetActive(true);

                break;
            case GameState.Player2SelectArmy:
                _player1Text.SetActive(false);
                _player2Text.SetActive(true);

                switch (GameManager.Instance.player1Faction)
                {
                    case Faction.Germany:
                        _germanSelection.SetActive(false);
                        break;
                    case Faction.GreatBritain:
                        _britishSelection.SetActive(false);
                        break;
                }

                break;
        }
    }

    public void onPlayerFactionChosen(Faction faction)
    {
        GameState currentGameState = GameManager.Instance.GetCurrentGameState();

        if (currentGameState == GameState.Player1SelectArmy)
        {
            GameManager.Instance.SetPlayerFaction(Player.Player1, faction);
        }
        else if (currentGameState == GameState.Player2SelectArmy)
        {
            GameManager.Instance.SetPlayerFaction(Player.Player2, faction);
        }
    }
}
