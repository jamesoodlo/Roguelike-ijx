using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "RoguelikeProject/UpgradeData", order = 0)]
public class UpgradeData : ScriptableObject
{
    [Header("Upgrade Cost")]
    public int upgradeHPCost;
    public int upgradeSTMCost;
    public int upgradeSTMReCost;
    public int upgradeGDCost;
    public int upgradeDMGCost;
    public int upgradeSPDCost;
    public int upgradePocket1Cost;
    public int upgradePocket2Cost;
    public int upgradeSuperDuckCost;
    public int upgradeSlasherCost;
}
