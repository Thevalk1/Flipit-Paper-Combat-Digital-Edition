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
    CharacterMovement,
    Player1ShootingTurn,
    Player2ShootingTurn,
    CharacterShooting,
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

    [SerializeField]
    private CoinBehavior _coinBehavior;

    [SerializeField]
    private GameObject _britainCamera;

    [SerializeField]
    private GameObject _germanyCamera;

    public GameObject _character;

    [SerializeField]
    private GameObject _britishSoldiers;

    [SerializeField]
    private GameObject _germanSoldiers;

    private Player coinTossWinner;
    public Player currentPlayer;

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
                _coinToss.SetActive(false);
                break;
            case GameState.Player2SelectArmy:
                _coinToss.SetActive(false);
                break;
            case GameState.CoinToss:
                _coinToss.SetActive(true);
                _coinBehavior.TossCoin();
                break;
            case GameState.Player1MovementTurn:
                _coinToss.SetActive(false);
                currentPlayer = Player.Player1;
                UpdateTurnCamera(Player.Player1);
                break;
            case GameState.Player2MovementTurn:
                _coinToss.SetActive(false);
                currentPlayer = Player.Player2;
                UpdateTurnCamera(Player.Player2);
                break;
            case GameState.CharacterMovement:
                UpdateCharacterCamera();
                break;
            case GameState.Player1ShootingTurn:
                currentPlayer = Player.Player1;
                UpdateTurnCamera(Player.Player1);
                break;
            case GameState.Player2ShootingTurn:
                currentPlayer = Player.Player2;
                UpdateTurnCamera(Player.Player2);
                break;
            case GameState.GameOver:
                break;
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

                UpdateGameState(GameState.CoinToss);
                break;
        }
    }

    public void StartNewTurn(Player firstPlayer)
    {
        coinTossWinner = firstPlayer;

        ProceedGameTurn();
    }

    public void ProceedGameTurn()
    {
        if (coinTossWinner == Player.Player1)
        {
            switch (State)
            {
                case GameState.CoinToss:
                    UpdateGameState(GameState.Player1MovementTurn);
                    break;
                case GameState.Player1MovementTurn:
                    UpdateGameState(GameState.Player2MovementTurn);
                    break;
                case GameState.Player2MovementTurn:
                    UpdateGameState(GameState.Player1ShootingTurn);
                    break;
                case GameState.Player1ShootingTurn:
                    UpdateGameState(GameState.Player2ShootingTurn);
                    break;
                case GameState.Player2ShootingTurn:
                    UpdateGameState(GameState.CoinToss);
                    break;
            }
        }
        else if (coinTossWinner == Player.Player2)
        {
            switch (State)
            {
                case GameState.CoinToss:
                    UpdateGameState(GameState.Player2MovementTurn);
                    break;
                case GameState.Player2MovementTurn:
                    UpdateGameState(GameState.Player1MovementTurn);
                    break;
                case GameState.Player1MovementTurn:
                    UpdateGameState(GameState.Player2ShootingTurn);
                    break;
                case GameState.Player2ShootingTurn:
                    UpdateGameState(GameState.Player1ShootingTurn);
                    break;
                case GameState.Player1ShootingTurn:
                    UpdateGameState(GameState.CoinToss);
                    break;
            }
        }
    }

    private void UpdateTurnCamera(Player player)
    {
        if (_character)
        {
            GameObject characterCamera = _character.transform.Find("Camera").gameObject;
            GameObject movementUI = _character.transform
                .Find("Character UI/Movement UI")
                .gameObject;
            movementUI.SetActive(false);
            characterCamera.SetActive(false);

            _character = null;
        }

        if (player == Player.Player1)
        {
            if (player1Faction == Faction.GreatBritain)
            {
                _britainCamera.SetActive(true);
                _germanyCamera.SetActive(false);
            }
            else
            {
                _germanyCamera.SetActive(true);
                _britainCamera.SetActive(false);
            }
        }
        else if (player == Player.Player2)
        {
            if (player2Faction == Faction.GreatBritain)
            {
                _britainCamera.SetActive(true);
                _germanyCamera.SetActive(false);
            }
            else
            {
                _germanyCamera.SetActive(true);
                _britainCamera.SetActive(false);
            }
        }
    }

    private void UpdateCharacterCamera()
    {
        _germanyCamera.SetActive(false);
        _britainCamera.SetActive(false);

        GameObject characterCamera = _character.transform.Find("Camera").gameObject;
        GameObject movementUI = _character.transform.Find("Character UI/Movement UI").gameObject;
        movementUI.SetActive(true);
        characterCamera.SetActive(true);
    }

    public void StartCharacterMovement(GameObject character)
    {
        _character = character;
        UpdateGameState(GameState.CharacterMovement);
    }

    public void FinishCharacterMovement()
    {
        if (currentPlayer == Player.Player1)
        {
            UpdateGameState(GameState.Player1MovementTurn);
        }
        else if (currentPlayer == Player.Player2)
        {
            UpdateGameState(GameState.Player2MovementTurn);
        }
    }
}
