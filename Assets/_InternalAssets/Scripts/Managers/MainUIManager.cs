using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager Instance;

    [SerializeField]
    private GameObject _mainUIPanel;

    [SerializeField]
    private TextMeshProUGUI _currentPlayer;

    [SerializeField]
    private TextMeshProUGUI _currentPhase;

    void Awake()
    {
        Instance = this;

        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
        _mainUIPanel.SetActive(false);
    }

    void onDestroy()
    {
        GameManager.onGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        _mainUIPanel.SetActive(
            state == GameState.Player1MovementTurn
                || state == GameState.Player2MovementTurn
                || state == GameState.Player1ShootingTurn
                || state == GameState.Player2ShootingTurn
        );

        switch (state)
        {
            case GameState.CoinToss:
                _currentPlayer.text = "";
                _currentPhase.text = "";
                _mainUIPanel.SetActive(false);
                break;
            case GameState.Player1MovementTurn:
                _currentPlayer.text = "Player 1";
                _currentPhase.text = "Movement Turn";
                _mainUIPanel.SetActive(true);
                break;
            case GameState.Player2MovementTurn:
                _currentPlayer.text = "Player 2"; 
                _currentPhase.text = "Movement Turn";
                _mainUIPanel.SetActive(true);
                break;
            case GameState.Player1ShootingTurn:
                _currentPlayer.text = "Player 1"; 
                _currentPhase.text = "Shooting Turn";
                break;
            case GameState.Player2ShootingTurn:
                _currentPlayer.text = "Player 2"; 
                _currentPhase.text = "Shooting Turn";
                break;
        }
    }
}
