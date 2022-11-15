using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    public static GridSystemVisual Instance {get; private set;}
    [SerializeField] private Transform gridSystemVisualSinglePrefab;
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
    }
    
    private void Update() {
        UpdateGridVisual();
    }

    public void HideAllGridPositions(){
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++){
            for(int z = 0; z < LevelGrid.Instance.GetHeight(); z++){
                gridSystemVisualSingle[x, z].Hide();
            }
        }
    }

    public void ShowGridPositionList(List<GridPosition> gridPositionList){
        foreach(GridPosition gridPosition in gridPositionList){
            gridSystemVisualSingle[gridPosition.x, gridPosition.z].Show();
        }
    }

    private void UpdateGridVisual(){
        HideAllGridPositions();
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        ShowGridPositionList(selectedUnit.GetMoveAction().GetValidActionGridPositionList());
    }
}
