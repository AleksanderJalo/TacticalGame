using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAI : MonoBehaviour
{
    private float timer;
    private void Start() {
        TurnSystem.Instance.TurnChange += TurnSystem_TurnChange;
    }
    private void Update()
    {
        if (TurnSystem.Instance.IsPlayerTurn()){
            return;
        }
        timer -= Time.deltaTime;
        if (timer <= 0){
            TurnSystem.Instance.NextTurn();
        }
    }

    private void TurnSystem_TurnChange(object sender, EventArgs e){
        timer = 2f;
    }
}
