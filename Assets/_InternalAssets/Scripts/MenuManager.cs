using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _armySelectPanel;

    [SerializeField]
    private TextMeshProUGUI _stateText;

    void Awake()
    {
        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
    }

    void onDestroy()
    {
        GameManager.onGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        _armySelectPanel.SetActive(state == GameState.SelectArmy);
    }

    void Start() { }

    public void EndTurn() { }
}
