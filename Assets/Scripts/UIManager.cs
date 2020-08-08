using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public CentralCore CentralTower;
    public TextMeshProUGUI TowerHealthUI;
    public TextMeshProUGUI TotalMoneyUI;
    public TextMeshProUGUI BoltTowerCostUI;
    public TextMeshProUGUI SlowTowerCostUI;
    public TextMeshProUGUI StasisTowerCostUI;
    public TextMeshProUGUI RewindTowerCostUI;

    void OnEnable()
    {
        UpdateUI();
        CentralCore.onDamage += UpdateUI;
    }


    private void OnDisable()
    {
        CentralCore.onDamage -= UpdateUI;
    }


    public void UpdateUI()
    {
        TowerHealthUI.text = CentralTower.health.ToString();
    }
}
