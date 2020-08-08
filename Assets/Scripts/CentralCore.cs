using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralCore : MonoBehaviour
{
    public PlayerData dataPlayer;

    // components
   

    /// <summary>
    /// Apply damage to core health
    /// </summary>
    /// <param name="dmg">amount of damage to deal</param>
    public void DamageCore(int dmg)
    {
        dataPlayer.DamageCore(dmg);

    }

    private void CheckIfDead()
    {
        if(dataPlayer.currentCoreHp <= 0)
        {
            GameObject.FindObjectOfType<LevelLoader>().LoadLevel("Game Over");
        }
    }


    private void OnEnable()
    {
        dataPlayer.onCoreHpChange += CheckIfDead;
            
    }

    private void OnDisable()
    {
        dataPlayer.onCoreHpChange -= CheckIfDead;
    }
}
