using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamage;
    [SerializeField] private int health = 100;
    private int healthMax; 
    public event EventHandler OnDeath;
    private void Awake() {
        healthMax = health;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        OnDamage?.Invoke(this, EventArgs.Empty);
        if (health < 0)
        {
            health = 0;
        }

        if (health == 0)
        {
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    public float GetHealthNormalized(){
        return (float) health / healthMax;
    }

}
