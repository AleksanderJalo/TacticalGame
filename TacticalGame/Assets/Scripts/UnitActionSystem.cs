using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance {get; private set;}
    public event EventHandler OnSelectedUnitChange;
    public event EventHandler OnSelectedActionChange;
    public event EventHandler<bool> OnBusyChange;
    public event EventHandler OnActionStarted;
    [SerializeField] private LayerMask unitLayerMask;
    [SerializeField] private Unit selectedUnit;

    private BaseAction selectedAction;
    private bool isBusy;
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        SetSelectedUnit(selectedUnit);
    }
   private void Update(){
    if(!TurnSystem.Instance.IsPlayerTurn()){
            return;
        }
    if(isBusy){
            return;
        }
    if(EventSystem.current.IsPointerOverGameObject()){
            return;
        }
    if(UnitSelection()){
        return;
        }
    HandleSelectedAction(); 
    
   }

   private bool UnitSelection(){
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
            {
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    if(unit == selectedUnit){
                        return false;
                    }

                    if(unit.IsEnemy()){
                        return false;
                    }
                    SetSelectedUnit(unit);
                    return true;
                }



            }
        }
        return false;
    

   }
   private void HandleSelectedAction(){
    if(Input.GetMouseButtonDown(0)){
        GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
        if (!selectedAction.IsValidActionGridPosition(mouseGridPosition)){
                return;
            }
        if(selectedUnit.TrySpendActionPointsToTakeAction(selectedAction)){
                SetBusy();
                selectedAction.TakeAction(mouseGridPosition, ClearBusy);
                OnActionStarted?.Invoke(this, EventArgs.Empty);
            }
    }

   }
    private void SetSelectedUnit(Unit unit){
        selectedUnit = unit;
        SetSelectedAction(unit.GetMoveAction());
        OnSelectedUnitChange?.Invoke(this, EventArgs.Empty);
   }

   public void SetSelectedAction(BaseAction baseAction){
        selectedAction = baseAction;
        OnSelectedActionChange?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit(){
        return selectedUnit;
   }

    private void SetBusy(){
        isBusy = true;
        OnBusyChange?.Invoke(this, isBusy);
    }
    private void ClearBusy(){
        isBusy = false;
        OnBusyChange?.Invoke(this, isBusy);
    }

    public BaseAction GetSelectedAction(){
        return selectedAction;
    }

}
