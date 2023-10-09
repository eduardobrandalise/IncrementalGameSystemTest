using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelUI : MonoBehaviour
{
    [SerializeField] private Button upgradeTravelSpeedButton;
    [SerializeField] private Button upgradeTransferTimeButton;
    [SerializeField] private TextMeshProUGUI travelSpeedText;
    [SerializeField] private TextMeshProUGUI transferTimeText;

    private TextMeshProUGUI _upgradeTravelSpeedButtonText;
    private TextMeshProUGUI _upgradeTransferTimeButtonText;
    
    private double _travelSpeed = 1f;
    private double _transferTime = 5f;
    private int _travelSpeedLevel = 1;
    private int _transferTimeLevel = 1;
    private double _travelSpeedRateGrowth = 1.15;
    private double _transferTimeRateGrowth = 1.07;
    private double _travelSpeedUpgradeBaseCost = 40;
    private double _transferTimeUpgradeBaseCost = 80;
    private double _travelSpeedNextUpgradeCost;
    private double _transferTimeNextUpgradeCost;
    private double _travelSpeedMultiplier = 1f;
    private double _transferTimeMultiplier = 1f;
    
    private void Start()
    {
        _upgradeTravelSpeedButtonText = upgradeTravelSpeedButton.GetComponentInChildren<TextMeshProUGUI>();
        _upgradeTransferTimeButtonText = upgradeTransferTimeButton.GetComponentInChildren<TextMeshProUGUI>();
        
        travelSpeedText.text = _travelSpeedUpgradeBaseCost.ToString(CultureInfo.InvariantCulture);
        transferTimeText.text = _transferTimeUpgradeBaseCost.ToString(CultureInfo.InvariantCulture);
        
        _travelSpeedNextUpgradeCost = _travelSpeedUpgradeBaseCost;
        _transferTimeNextUpgradeCost = _transferTimeUpgradeBaseCost;
        
        upgradeTravelSpeedButton.onClick.AddListener(UpgradeTravelSpeed);
        upgradeTransferTimeButton.onClick.AddListener(UpgradeTransferTime);
    }
    
    private void LateUpdate()
    {
        travelSpeedText.text = "Travel speed: " + _travelSpeed.ToString("F");
        transferTimeText.text = "Transfer time: " + _transferTime.ToString("F");

        _upgradeTravelSpeedButtonText.text = "Upgrade Travel Speed" + "\nCost: " + _travelSpeedNextUpgradeCost.ToString("F2");
        _upgradeTransferTimeButtonText.text = "Upgrade Transfer Time" + "\nCost: " + _transferTimeNextUpgradeCost.ToString("F2");
    }

    private void UpgradeTravelSpeed()
    {
        _travelSpeedLevel++;
        _travelSpeed = CalculateProduction(UpgradeType.TravelSpeed);
        _travelSpeedNextUpgradeCost = CalculateNextLevelCost(UpgradeType.TravelSpeed);
        print(_travelSpeedLevel);
    }
    
    private void UpgradeTransferTime()
    {
        _transferTimeLevel++;
        _transferTime = CalculateProduction(UpgradeType.TransferTime);
        _transferTimeNextUpgradeCost = CalculateNextLevelCost(UpgradeType.TransferTime);
        print(_transferTimeLevel);
    }

    private double CalculateNextLevelCost(UpgradeType upgradeType)
    {
        double nextUpgradeCost = 0;
        
        switch (upgradeType)
        {
            case UpgradeType.TravelSpeed:
                nextUpgradeCost = _travelSpeedUpgradeBaseCost * Mathf.Pow((float)_travelSpeedRateGrowth,_travelSpeedLevel);
                break;
            case UpgradeType.TransferTime:
                nextUpgradeCost = _transferTimeUpgradeBaseCost * Mathf.Pow((float)_transferTimeRateGrowth,_transferTimeLevel);
                break;
        }

        return nextUpgradeCost;
    }

    private double CalculateProduction(UpgradeType upgradeType)
    {
        double nextProduction = 0;
        
        switch (upgradeType)
        {
            case UpgradeType.TravelSpeed:
                nextProduction = (_travelSpeed * _travelSpeedLevel) * _travelSpeedMultiplier;
                break;
            case UpgradeType.TransferTime:
                nextProduction = (_transferTime * _transferTimeLevel) * _transferTimeMultiplier;
                break;
        }

        return nextProduction;
    }
}
