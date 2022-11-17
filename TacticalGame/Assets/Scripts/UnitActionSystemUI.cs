using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;

    private void Start() {
        
        UnitActionSystem.Instance.OnSelectedUnitChange += UnitActionSystem_OnSelectedUnitChange;
        CreateUnitActionButtons();
    }
    private void CreateUnitActionButtons(){
        foreach(Transform buttonTransform in actionButtonContainerTransform){
            Destroy(buttonTransform.gameObject);
        }

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach(BaseAction baseAction in selectedUnit.GetBaseActionArray()){
            Transform actionButtonTransfrom = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            ActionButtonUI actionButtonUI = actionButtonTransfrom.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction);
        }

    }

    private void UnitActionSystem_OnSelectedUnitChange(object sender, EventArgs e){

        CreateUnitActionButtons();
    } 
}
