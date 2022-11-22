using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private Button endTurnButton;
    [SerializeField] private TextMeshProUGUI turnNumberText;
    [SerializeField] private GameObject enemyTurnVisualGameObject;
    private void Start() {
        UpdateEndTurnButtonVisibility();
        UpdateEnemyTurnVisual();
        UpdateTurnText();
        endTurnButton.onClick.AddListener(() =>
        {
            TurnSystem.Instance.NextTurn();
        });

        TurnSystem.Instance.TurnChange += TurnSystem_TurnChange;
    }

    private void UpdateTurnText(){
        turnNumberText.text = "Turn " + TurnSystem.Instance.GetTurnNumber();
      
    }

    private void TurnSystem_TurnChange(object sender, EventArgs e){
        UpdateTurnText();
        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonVisibility();
    }
    private void UpdateEnemyTurnVisual(){
        enemyTurnVisualGameObject.SetActive(!TurnSystem.Instance.IsPlayerTurn());
    }
    private void UpdateEndTurnButtonVisibility(){
        endTurnButton.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn());
    }
}
