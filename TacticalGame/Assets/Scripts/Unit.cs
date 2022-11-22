using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
    public static event EventHandler OnAnyActionPointsChange;
    private const int ACTION_POINTS_MAX = 2;
    private SpinAction spinAction;
    private GridPosition gridPosition;
    private MoveAction moveAction;
    private BaseAction[] baseActionArray;
    private int actionPoints = 2;
    private void Awake() {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        baseActionArray = GetComponents<BaseAction>();
    }
    private void Start() {
        TurnSystem.Instance.TurnChange += TurnSystem_TurnChange;
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
    }
    private void Update(){
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if(newGridPosition != gridPosition){
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }

       
    }

    public MoveAction GetMoveAction(){
        return moveAction;
    }

    public SpinAction GetSpinAction(){
        return spinAction;
    }

    public GridPosition GetGridPosition(){
        return gridPosition;
    }

    public BaseAction[] GetBaseActionArray(){
        return baseActionArray;
    }

    public bool CanSpendActionPointsToTakeAction(BaseAction baseAction){
        if(actionPoints >= baseAction.GetActionPointsCost()){
            return true;
        } else {
            return false;
        }
    }

    private void SpendActionPoints(int amount){
        actionPoints -= amount;
        OnAnyActionPointsChange?.Invoke(this, EventArgs.Empty);
    }

    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction){
        if(CanSpendActionPointsToTakeAction(baseAction)){
            SpendActionPoints(baseAction.GetActionPointsCost());
            return true;
        } else {
            return false;
        }
    }


    public int GetActionPoints(){
        return actionPoints;
    }

    private void TurnSystem_TurnChange(object sender, EventArgs e){
        actionPoints = ACTION_POINTS_MAX;
        OnAnyActionPointsChange?.Invoke(this, EventArgs.Empty);
    }
 
}
