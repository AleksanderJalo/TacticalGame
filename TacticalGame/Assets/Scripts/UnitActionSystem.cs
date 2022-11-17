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
    private bool isBusy;
    private void Awake() {
        Instance = this;
    }
   private void Update(){
    if(isBusy){
            return;
        }

    if(Input.GetKeyDown(KeyCode.Mouse0)){
            if(UnitSelection()) return;
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            if(selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition)){

                SetBusy();
                selectedUnit.GetMoveAction().Move(mouseGridPosition, ClearBusy);
            }
            
        }
    if(Input.GetKeyDown(KeyCode.Mouse1)){
            SetBusy();
            selectedUnit.GetSpinAction().Spin(ClearBusy);
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

    private void SetBusy(){
        isBusy = true;
    }
    private void ClearBusy(){
        isBusy = false;
    }

}
