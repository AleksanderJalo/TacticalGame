using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAI : MonoBehaviour
{
    private float timer;
    private enum State{
        WaitingForEnemyTurn,
        TakingTurn,
        Busy
    }

    private State state;
    private void Awake() {
        state = State.WaitingForEnemyTurn;
    }
    private void Start() {
        TurnSystem.Instance.TurnChange += TurnSystem_TurnChange;
    }
    private void Update()
    {
        if (TurnSystem.Instance.IsPlayerTurn()){
            return;
        }

        switch(state){
            case State.WaitingForEnemyTurn:
                break;
            case State.TakingTurn:
                timer -= Time.deltaTime;
                if (timer <= 0){
                    state = State.Busy;
                    if(TryTakeEnemyAiAction(SetStateTakingTurn)){
                        state = State.Busy;
                    }else{
                        TurnSystem.Instance.NextTurn();
                    }
                }
                break;
            case State.Busy:
                break;
        }
        
    }


    private void SetStateTakingTurn(){
        timer = 0.5f;
        state = State.TakingTurn;
    }

    private void TurnSystem_TurnChange(object sender, EventArgs e){
        if (!TurnSystem.Instance.IsPlayerTurn()){
            state = State.TakingTurn;
            timer = 2f;
        }

    }
    private bool TryTakeEnemyAiAction(Unit enemyUnit, Action onEnemyAiActionComplete){
        EnemyAiAction bestEnemyAiAction = null;
        BaseAction bestBaseAction = null;
        foreach( BaseAction baseAction in  enemyUnit.GetBaseActionArray()){
            if(!enemyUnit.CanSpendActionPointsToTakeAction(baseAction)){
                continue;
            }
            if(bestBaseAction == null){
                bestEnemyAiAction = baseAction.GetBestEnemyAiAction();
                bestBaseAction = baseAction;
            }else{
                EnemyAiAction testEnemyAiAction = baseAction.GetBestEnemyAiAction();
                if(testEnemyAiAction!= null && testEnemyAiAction.actionValue > bestEnemyAiAction.actionValue){
                    bestEnemyAiAction = testEnemyAiAction;
                    bestBaseAction = baseAction;
                }
            }
        }
        if(bestEnemyAiAction != null && enemyUnit.TrySpendActionPointsToTakeAction(bestBaseAction)){
            bestBaseAction.TakeAction(bestEnemyAiAction.gridPosition, onEnemyAiActionComplete);
            return true;
        }else{
            return false;
        }
       
    }

    private bool TryTakeEnemyAiAction(Action onEnemyAiActionComplete){
        foreach(Unit enemyUnit in UnitManager.Instance.GetEnemyUnitList()){
            if(TryTakeEnemyAiAction(enemyUnit, onEnemyAiActionComplete)){
                 return true;
            }
           
        }
        return false;
    }
}
