using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    SelectArmy,
    CoinToss,
    Player1MovementTurn,
    Player2MovementTurn,
    Player1ShootingTurn,
    Player2ShootingTurn,
    GameOver,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> onGameStateChanged;

    void Awake()
    {
        Instance = this;
    }

    void Start() {
        UpdateGameState(GameState.SelectArmy);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.SelectArmy:
                break;
            case GameState.CoinToss:
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
}
