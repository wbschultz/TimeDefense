﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralCore : MonoBehaviour
{
    // health of core
    [SerializeField]
    private float health = 100f;

    // components

    //events
    public static event Action onDeath;
    public static event Action onDamage;

    private void Update()
    {
        if(health <= 0)
        {
            if (onDeath != null)
            {
                onDeath.Invoke();
            }
        }
    }

    /// <summary>
    /// Apply damage to core health
    /// </summary>
    /// <param name="dmg">amount of damage to deal</param>
    public void DamageCore(float dmg)
    {
        health -= dmg;
        if (onDamage != null)
        {
            onDamage.Invoke();
        }

    }
}