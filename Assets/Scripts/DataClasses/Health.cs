using System;
using UnityEngine;

[Serializable]
public class Health
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    
    public int CurrentHealth { 
        get => currentHealth; 
        private set 
        {
            currentHealth = value;
            NotifyChanges?.Invoke(this);
        }
    }

    public int MaxHealth 
    { 
        get => maxHealth; 
        private set 
        { 
            maxHealth = value; 
            NotifyChanges?.Invoke(this);
        }
    }

    public void SetDamage(int damage)
    {
        if (damage < 0) throw new ArgumentException(nameof(damage), "Damage must be greater than zero");
        if (currentHealth - damage < 0) return;

        CurrentHealth -= damage;
    }

    public Action<Health> NotifyChanges;
}