using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridSystemVisual : MonoBehaviour
{
    public static GridSystemVisual Instance {get; private set;}

    [Serializable]
    public struct GridVisualTypeMaterial{
        public GridVisualType gridVisualType;
        public Material material;
    }
    public enum GridVisualType
    {
        White,
        Blue,
        Red,
        RedSoft,
        Yellow

    }
    [SerializeField] private Transform gridSystemVisualSinglePrefab;
    [SerializeField] private List<GridVisualTypeMaterial> gridVisualTypeMaterials;
    private GridSystemVisualSingle[,] gridSystemVisualSingle;
    private void Awake() {
        if(Instance!=null){
            Debug.Log("There is more then one UnitActionSystem");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start() {
        gridSystemVisualSingle = new GridSystemVisualSingle[LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight()];
        for(int x = 0; x < LevelGrid.Instance.GetWidth(); x++){
            for(int z = 0; z < LevelGrid.Instance.GetHeight(); z++){
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridSystemVisualSingleTransform = Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
                gridSystemVisualSingle[x, z] = gridSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
            }
        }
        UpdateGridVisual();
        UnitActionSystem.Instance.OnSelectedActionChange += UnitActionSystem_OnSelectedActionChange;
        LevelGrid.Instance.OnAnyUnitMoveGridPostion += LevelGrid_OnAnyUnitMoveGridPosition;
    }

    private void UnitActionSystem_OnSelectedActionChange(object sender, EventArgs e){
        UpdateGridVisual();
    }

    private void LevelGrid_OnAnyUnitMoveGridPosition(object sender, EventArgs e){
        UpdateGridVisual();
    }

    public void HideAllGridPositions(){
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++){
            for(int z = 0; z < LevelGrid.Instance.GetHeight(); z++){
                gridSystemVisualSingle[x, z].Hide();
            }
        }
    }

     private void ShowGridPositionRange(GridPosition gridPosition, int range, GridVisualType gridVisualType)
    {
        List<GridPosition> gridPositionList = new List<GridPosition>();

        for (int x = -range; x <= range; x++)
        {
            for (int z = -range; z <= range; z++)
            {
                GridPosition testGridPosition = gridPosition + new GridPosition(x, z);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > range)
                {
                    continue;
                }

                gridPositionList.Add(testGridPosition);
            }
        }

        ShowGridPositionList(gridPositionList, gridVisualType);
    }


    public void ShowGridPositionList(List<GridPosition> gridPositionList, GridVisualType gridVisualType){
        foreach(GridPosition gridPosition in gridPositionList){
            gridSystemVisualSingle[gridPosition.x, gridPosition.z].Show(GetGridVisualTypeMaterial(gridVisualType));
        }
    }

    private void UpdateGridVisual(){
        HideAllGridPositions();

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();
        GridVisualType gridVisualType;
        switch(selectedAction){
            default:
            case MoveAction moveAction:
                gridVisualType = GridVisualType.White;
                break;
            case SpinAction spinAction:
                gridVisualType = GridVisualType.Blue;
                break;
            case ShootAction shootAction:
                gridVisualType = GridVisualType.Red;

                ShowGridPositionRange(selectedUnit.GetGridPosition(), shootAction.GetMaxShootDistance(), GridVisualType.RedSoft);
                break;
        }
        ShowGridPositionList(selectedAction.GetValidActionGridPositionList(), gridVisualType);
    }

    private Material GetGridVisualTypeMaterial(GridVisualType gridVisualType){
        foreach(GridVisualTypeMaterial gridVisualTypeMaterial in gridVisualTypeMaterials){
            if(gridVisualTypeMaterial.gridVisualType == gridVisualType){
                return gridVisualTypeMaterial.material;
            }
        }
        Debug.Log("Could not find material");
        return null; 
    }
}
