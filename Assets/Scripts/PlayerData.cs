using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    public int maxPlayerHp = 50;
    public int maxCoreHp = 200;
    public int startMunny = 100;
    public int maxMunny = 999;

    public int currentPlayerHp { get; private set; }
    public int currentCoreHp { get; private set; }
    public int currentMunny { get; private set; }

    public event Action onPlayerDeath;
    public event Action onPlayerHpChange;
    public event Action onCoreDeath;
    public event Action onCoreHpChange;
    public event Action onMunnyChange;

    private void OnEnable()
    {
        // Initialize player's current data.
        currentPlayerHp = maxPlayerHp;
        currentCoreHp = maxCoreHp;
        currentMunny = startMunny;

        // Let all listeners know player's data has changed.
        if (onPlayerHpChange != null) onPlayerHpChange.Invoke();
        if (onCoreHpChange != null) onCoreHpChange.Invoke();
        if (onMunnyChange != null) onMunnyChange.Invoke();
    }

    // Deal damage to player.
    public void DamagePlayer(int dmg)
    {
        currentPlayerHp -= dmg;
        if(onPlayerHpChange != null) onPlayerHpChange.Invoke();

        if (currentPlayerHp <= 0)
        {
            if(onPlayerDeath != null) onPlayerDeath.Invoke();
        }
    }

    // Deal damage to tower core.
    public void DamageCore(int dmg)
    {
        currentCoreHp -= dmg;
        if (onCoreHpChange != null) onCoreHpChange.Invoke();

        if(currentCoreHp <= 0)
        {
           if (onCoreDeath != null) onCoreDeath.Invoke();
        }
    }

    // Check if enough money to spend amount.
    public bool EnoughMoney(int amount)
    {
        if (amount > currentMunny)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // Spend money to purchase something.
    public void SpendMoney(int munnySpent)
    {
        currentMunny -= munnySpent;
        if (onMunnyChange != null) onMunnyChange.Invoke();
    }

    // Gained money.
    public void GainMoney(int munnyGained)
    {
        currentMunny += munnyGained;

        if (currentMunny > maxMunny)
        {
            currentMunny = maxMunny;
        }

        if (onMunnyChange != null) onMunnyChange.Invoke();
    }
}
