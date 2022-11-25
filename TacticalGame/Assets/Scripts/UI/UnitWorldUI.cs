using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionPointText;
    [SerializeField] private Unit unit;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private HealthSystem healthSystem;

    private void Start() {
        UpdateActionPointsText();
        UpdateHealthBar();
        Unit.OnAnyActionPointsChange += Unit_OnAnyActionPointChange;
        healthSystem.OnDamage += HealthSystem_OnDamage;
    }
    private void UpdateActionPointsText(){
        actionPointText.text = unit.GetActionPoints().ToString();
    }

    private void Unit_OnAnyActionPointChange(object sender, EventArgs e){
        UpdateActionPointsText();
    }

    private void UpdateHealthBar(){
        healthBarImage.fillAmount = healthSystem.GetHealthNormalized();
    }
    
    private void HealthSystem_OnDamage(object sender, EventArgs e){
        UpdateHealthBar();
    }
}

