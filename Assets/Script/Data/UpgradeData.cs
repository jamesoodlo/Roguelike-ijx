using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "RoguelikeProject/UpgradeData", order = 0)]
public class UpgradeData : ScriptableObject
{
    [Header("Upgrade Cost")]
    public int upgradeHPCost;
    public int upgradeSHDCost;
    public int upgradeDMGCost;
    public int upgradeSPDCost;
    public int upgradeBarrierCost;
    public int upgradeSlasherCost;
}
