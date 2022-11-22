using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;
    [SerializeField] private TextMeshProUGUI actionPointsText;
    private List<ActionButtonUI> actionButtonUIList;

    private void Start() {
        actionButtonUIList = new List<ActionButtonUI>();
        TurnSystem.Instance.TurnChange += TurnSystem_TurnChange;
        UnitActionSystem.Instance.OnSelectedUnitChange += UnitActionSystem_OnSelectedUnitChange;
        UnitActionSystem.Instance.OnSelectedActionChange += UnitActionSystem_OnSelectedActionChange;
        UnitActionSystem.Instance.OnActionStarted += UnitActionSystem_OnSelectedActionStarted;
        Unit.OnAnyActionPointsChange += Unit_OnAnyActionPointChange;
        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPoints();
    }
    private void CreateUnitActionButtons(){
        foreach(Transform buttonTransform in actionButtonContainerTransform){
            Destroy(buttonTransform.gameObject);
        }

        actionButtonUIList.Clear();

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach(BaseAction baseAction in selectedUnit.GetBaseActionArray()){
            Transform actionButtonTransfrom = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            ActionButtonUI actionButtonUI = actionButtonTransfrom.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction);
            actionButtonUIList.Add(actionButtonUI);
        }

    }

    private void UnitActionSystem_OnSelectedUnitChange(object sender, EventArgs e){

        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPoints();
    }

    private void UnitActionSystem_OnSelectedActionChange(object sender, EventArgs e){
        UpdateSelectedVisual();
    } 

    private void UnitActionSystem_OnSelectedActionStarted(object sender, EventArgs e){
        UpdateActionPoints();
    }
    private void TurnSystem_TurnChange(object sender, EventArgs e){
        UpdateActionPoints();
    }

    private void Unit_OnAnyActionPointChange(object sender, EventArgs e){
        UpdateActionPoints();
    }

    private void UpdateSelectedVisual(){
        foreach(ActionButtonUI actionButtonUI in actionButtonUIList){
            actionButtonUI.UpdateSelectedVisual();
        }
    }

    private void UpdateActionPoints(){
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        actionPointsText.text = "Action Points: " + selectedUnit.GetActionPoints();
    }
}
