using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
    private SpinAction spinAction;
    private GridPosition gridPosition;
    private MoveAction moveAction;
    private void Awake() {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
    }
    private void Start() {
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
 
}
