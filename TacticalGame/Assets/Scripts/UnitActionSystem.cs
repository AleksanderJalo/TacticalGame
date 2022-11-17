using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance {get; private set;}
    public event EventHandler OnSelectedUnitChange;
    [SerializeField] private LayerMask unitLayerMask;
    [SerializeField] private Unit selectedUnit;
    private void Awake() {
        Instance = this;
    }
   private void Update(){
    
    if(Input.GetKeyDown(KeyCode.Mouse0)){
            if(UnitSelection()) return;
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            if(selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition)){
                
                selectedUnit.GetMoveAction().Move(mouseGridPosition);
            }
            
        }
    if(Input.GetKeyDown(KeyCode.Mouse1)){
            selectedUnit.GetSpinAction().Spin();
        } 
    
   }

   private bool UnitSelection(){
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask)){
        SetSelectedUnit(raycastHit);
        return true;

        
    }else{
        return false;
    }

   }
   private void SetSelectedUnit(RaycastHit raycastHit){
    selectedUnit = raycastHit.transform.GetComponent<Unit>();
    OnSelectedUnitChange?.Invoke(this, EventArgs.Empty);
   }
   public Unit GetSelectedUnit(){
    return selectedUnit;
   }
}
