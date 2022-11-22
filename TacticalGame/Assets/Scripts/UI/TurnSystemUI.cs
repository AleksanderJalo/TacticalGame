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
    private void Start() {
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
    }
}
