using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _armySelectPanel;

    [SerializeField]
    private TExtMeshProUGUI _stateText;

    void Awake()
    {
        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
    }

    void onDestroy()
    {
        GameManager.onGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state) { 
        _armySelectPanel.SetActive(state == GameState.SelectArmy);
    }

    void Start() { }

    public void EndTurn() { }
}
