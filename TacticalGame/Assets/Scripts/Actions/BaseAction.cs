using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseAction : MonoBehaviour
{
    protected Unit unit;
    protected bool isActive = false;
    protected Action onActionComplete;
    protected virtual void Awake() {
        unit = GetComponent<Unit>();
    }
}
