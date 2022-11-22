using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance{get; private set;}
    [SerializeField] private Transform gridDebugObjectPrefab;
    private GridSystem gridSystem;
    private void Awake() {
        gridSystem = new GridSystem(10, 10, 2f);
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
        Instance = this;
    }


    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit){
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }

    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition){
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnitList();
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit){
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition){
        return gridSystem.getGridPosition(worldPosition);

    }

    public Vector3 GetWorldPosition(GridPosition gridPosition){
        return gridSystem.getWorldPosition(gridPosition);

    }

    public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPostition, GridPosition toGridPosition){
        RemoveUnitAtGridPosition(fromGridPostition, unit);
        AddUnitAtGridPosition(toGridPosition, unit);
    }

    public bool IsValidGridPosition (GridPosition gridPosition){
        return gridSystem.IsValidGridPosition(gridPosition);
    }

    public bool HasAnyUnitAtThisPosition(GridPosition gridPosition){
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.HasAnyUnits();

    }
    public Unit GetUnitAtGridPosition(GridPosition gridPosition){
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnit();
    }

    public int GetWidth(){
        return gridSystem.GetWidth();
    }
      public int GetHeight(){
        return gridSystem.GetHeight();
    }
}
