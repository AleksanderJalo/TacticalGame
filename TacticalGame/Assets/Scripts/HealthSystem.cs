using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int health = 100;
    public event EventHandler OnDeath;

    public void Damage(int damageAmount){
        health -= damageAmount;
        if (health < 0){
            health = 0;
        }

        if (health == 0){
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }

}
