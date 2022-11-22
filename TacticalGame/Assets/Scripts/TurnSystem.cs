using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnSystem : MonoBehaviour
{
    public event EventHandler TurnChange;
    public static TurnSystem Instance { get; private set;}
    private int turnNumber = 1;
    private void Awake() {
        Instance = this;
    }

    public void NextTurn(){
        turnNumber++;
        TurnChange?.Invoke(this, EventArgs.Empty);
    }

    public int GetTurnNumber(){
        return turnNumber;
    }
}
