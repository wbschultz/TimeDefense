using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerData dataPlayer;
    public TextMeshProUGUI TowerHealthUI;
    public TextMeshProUGUI TotalMoneyUI;
    public TextMeshProUGUI BoltTowerCostUI;
    public TextMeshProUGUI SlowTowerCostUI;
    public TextMeshProUGUI StasisTowerCostUI;
    public TextMeshProUGUI RewindTowerCostUI;

    void OnEnable()
    {
        UpdateUI();
        dataPlayer.onCoreHpChange += UpdateUI;
        dataPlayer.onMunnyChange += UpdateUI;

    }


    private void OnDisable()
    {
        dataPlayer.onCoreHpChange -= UpdateUI;
        dataPlayer.onMunnyChange -= UpdateUI;
    }


    public void UpdateUI()
    {
        TowerHealthUI.text = dataPlayer.currentCoreHp.ToString();
        TotalMoneyUI.text = dataPlayer.currentMunny.ToString();

    }
}
