using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
    [SerializeField] private bool isEnemy;
    private HealthSystem healthSystem;
    public static event EventHandler OnAnyActionPointsChange;
    public static event EventHandler OnAnyUnitSpawn;
    public static event EventHandler OnAnyUnitDeath;
    private const int ACTION_POINTS_MAX = 2;
    private ShootAction shootAction;
    private SpinAction spinAction;
    private GridPosition gridPosition;
    private MoveAction moveAction;
    private BaseAction[] baseActionArray;
    private int actionPoints = 2;
    private void Awake() {
        shootAction = GetComponent<ShootAction>();
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        baseActionArray = GetComponents<BaseAction>();
        healthSystem = GetComponent<HealthSystem>();
    }
    private void Start() {
        TurnSystem.Instance.TurnChange += TurnSystem_TurnChange;
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
        healthSystem.OnDeath += HealthSystem_OnDeath;
        OnAnyUnitSpawn?.Invoke(this, EventArgs.Empty);
    }
    private void Update(){
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if(newGridPosition != gridPosition){
            GridPosition oldGridPosition = gridPosition;
            gridPosition = newGridPosition;
            LevelGrid.Instance.UnitMovedGridPosition(this, oldGridPosition, newGridPosition);
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
        if (IsEnemy() && !TurnSystem.Instance.IsPlayerTurn() || 
            !IsEnemy() && TurnSystem.Instance.IsPlayerTurn())
        {
            actionPoints = ACTION_POINTS_MAX;
            OnAnyActionPointsChange?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsEnemy(){
        return isEnemy;
    }

    public void Damage(int damageAmount){
        Debug.Log("boom");
        healthSystem.Damage(damageAmount);
    }
    public Vector3 GetWorldPosition(){
        return transform.position;
    }

    private void HealthSystem_OnDeath(object sender, EventArgs e){
        LevelGrid.Instance.RemoveUnitAtGridPosition(gridPosition, this);
        Destroy(gameObject);
        OnAnyUnitDeath?.Invoke(this, EventArgs.Empty);
    }

    public ShootAction GetShootAction(){
        return shootAction;
    }

    public float GetHealthNormalized(){
        return healthSystem.GetHealthNormalized();
    }
 
}
