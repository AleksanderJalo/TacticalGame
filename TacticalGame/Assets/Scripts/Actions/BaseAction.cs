using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseAction : MonoBehaviour
{
    public static event EventHandler OnAnyActionStarted;
    public static event EventHandler OnAnyActionCompleted;
    protected Unit unit;
    protected bool isActive = false;
    protected Action onActionComplete;
    protected virtual void Awake() {
        unit = GetComponent<Unit>();
    }

    public abstract string GetActionName();
    public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);
    public virtual bool IsValidActionGridPosition(GridPosition gridPosition){
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }
    public abstract List<GridPosition> GetValidActionGridPositionList();

    public virtual int GetActionPointsCost(){
        return 1;
    }
        protected void ActionStart(Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;
        OnAnyActionStarted?.Invoke(this, EventArgs.Empty);
    }

    protected void ActionComplete()
    {
        isActive = false;
        onActionComplete();
        OnAnyActionCompleted?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetUnit(){
        return unit;
    }

    public EnemyAiAction GetBestEnemyAiAction(){
        List<EnemyAiAction> enemyAiActionList = new List<EnemyAiAction>();
        List<GridPosition> validActionGridPoisition = GetValidActionGridPositionList();
        foreach(GridPosition gridPosition in validActionGridPoisition){
            EnemyAiAction enemyAiAction = GetEnemyAiAction(gridPosition);
            enemyAiActionList.Add(enemyAiAction);
            
        }
        if (enemyAiActionList.Count > 0)
        {
            enemyAiActionList.Sort((EnemyAiAction a, EnemyAiAction b) => b.actionValue - a.actionValue);
            return enemyAiActionList[0];
        }else{
            return null;
        }
    }

    public abstract EnemyAiAction GetEnemyAiAction(GridPosition gridPosition);




}
