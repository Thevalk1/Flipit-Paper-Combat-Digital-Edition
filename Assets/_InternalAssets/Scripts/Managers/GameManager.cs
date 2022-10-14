using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Player1SelectArmy,
    Player2SelectArmy,
    CoinToss,
    Player1MovementTurn,
    Player2MovementTurn,
    Player1ShootingTurn,
    Player2ShootingTurn,
    GameOver,
}

public enum Player
{
    Player1,
    Player2,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> onGameStateChanged;

    public Faction player1Faction;
    public Faction player2Faction;

    [SerializeField]
    private GameObject _coinToss;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.Player1SelectArmy);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Player1SelectArmy:
                break;
            case GameState.Player2SelectArmy:
                break;
            case GameState.CoinToss:
                _coinToss.SetActive(true);
                break;
            case GameState.Player1MovementTurn:
                break;
            case GameState.Player2MovementTurn:
                break;
            case GameState.Player1ShootingTurn:
                break;
            case GameState.Player2ShootingTurn:
                break;
            case GameState.GameOver:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        onGameStateChanged?.Invoke(newState); // If any object is subscribed, then invoke the function
    }

    public GameState GetCurrentGameState()
    {
        return State;
    }

    public void SetPlayerFaction(Player player, Faction faction)
    {
        switch (player)
        {
            case Player.Player1:
                player1Faction = faction;
                UpdateGameState(GameState.Player2SelectArmy);
                break;
            case Player.Player2:
                player2Faction = faction;

                Debug.Log("Player 1 Faction " + player1Faction);
                Debug.Log("Player 2 Faction " + player2Faction);

                UpdateGameState(GameState.CoinToss);
                break;
        }
    }
}
