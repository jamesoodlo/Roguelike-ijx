using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    [Header("Data")]
    public StageData stageData;
    public BaseStatus playerDataStat;
    public SettingData settingData;
    public UpgradeData upgradeCost;
   
    public SaveData saveData;

    private void Update() 
    {
        saveData.maxHealth = playerDataStat.maxHealth;
        saveData.maxStamina = playerDataStat.maxStamina;
        saveData.maxStaminaRegen = playerDataStat.maxStaminaRegen;
        saveData.maxGuard = playerDataStat.maxGuard;
        saveData.attackDamage = playerDataStat.attackDamage; 
        saveData.speed = playerDataStat.speed;

        saveData.item1Pocket = playerDataStat.item1Pocket;
        saveData.item2Pocket = playerDataStat.item2Pocket;

        saveData.hasSlasher = playerDataStat.hasSlasher;
        saveData.hasSuperDuck = playerDataStat.hasSuperDuck;

        saveData.upgradeHPCost = upgradeCost.upgradeHPCost;
        saveData.upgradeSTMCost = upgradeCost.upgradeSTMCost;
        saveData.upgradeSTMReCost = upgradeCost.upgradeSTMReCost;
        saveData.upgradeGDCost = upgradeCost.upgradeGDCost;
        saveData.upgradeDMGCost = upgradeCost.upgradeDMGCost;
        saveData.upgradeSPDCost = upgradeCost.upgradeSPDCost;
        saveData.upgradeSuperDuckCost = upgradeCost.upgradeSuperDuckCost;
        saveData.upgradeSlasherCost = upgradeCost.upgradeSlasherCost;
        saveData.upgradePocket1Cost = upgradeCost.upgradePocket1Cost;
        saveData.upgradePocket2Cost = upgradeCost.upgradePocket2Cost;

        saveData.musicSound = settingData.musicSound;
        saveData.ambientSound = settingData.ambientSound;
        saveData.effectSound = settingData.effectSound;
        saveData.uiSound = settingData.uiSound;
        saveData.qualityIndex = settingData.qualityIndex;
        saveData.isFullScreen = settingData.isFullScreen;

        saveData.isTutorial = settingData.isTutorial;
        saveData.notNewGame = settingData.isTutorial;

    }

    public void SaveGame()
    {
        //BinaryFormatter bf = new BinaryFormatter(); 
        //FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat"); 
        SaveData data = new SaveData();

        data.maxHealth = playerDataStat.maxHealth;
        data.maxStamina = playerDataStat.maxStamina;
        data.maxStaminaRegen = playerDataStat.maxStaminaRegen;
        data.maxGuard = playerDataStat.maxGuard;
        data.attackDamage = playerDataStat.attackDamage; 
        data.speed = playerDataStat.speed;

        data.item1Pocket = playerDataStat.item1Pocket;
        data.item2Pocket = playerDataStat.item2Pocket;

        data.hasSlasher = playerDataStat.hasSlasher;
        data.hasSuperDuck = playerDataStat.hasSuperDuck;

        data.upgradeHPCost = upgradeCost.upgradeHPCost;
        data.upgradeSTMCost = upgradeCost.upgradeSTMCost;
        data.upgradeSTMReCost = upgradeCost.upgradeSTMReCost;
        data.upgradeGDCost = upgradeCost.upgradeGDCost;
        data.upgradeDMGCost = upgradeCost.upgradeDMGCost;
        data.upgradeSPDCost = upgradeCost.upgradeSPDCost;
        data.upgradeSuperDuckCost = upgradeCost.upgradeSuperDuckCost;
        data.upgradeSlasherCost = upgradeCost.upgradeSlasherCost;
        data.upgradePocket1Cost = upgradeCost.upgradePocket1Cost;
        data.upgradePocket2Cost = upgradeCost.upgradePocket2Cost;

        data.musicSound = settingData.musicSound;
        data.ambientSound = settingData.ambientSound;
        data.effectSound = settingData.effectSound;
        data.uiSound = settingData.uiSound;
        data.qualityIndex = settingData.qualityIndex;
        data.isFullScreen = settingData.isFullScreen;

        data.isTutorial = settingData.isTutorial;
        data.notNewGame = settingData.isTutorial;

        string saveDataString = JsonUtility.ToJson(data);

        PlayerPrefs.SetString("Save", saveDataString);
        PlayerPrefs.Save();

        //bf.Serialize(file, data);
        //file.Close();
        Debug.Log("Game data saved!");
        
    }

    public void LoadGame()
    {
        string loadDataString = PlayerPrefs.GetString("Save");   
        SaveData loadSave = JsonUtility.FromJson<SaveData>(loadDataString);

        if (loadSave != null)
        {
            //File.Exists(Application.persistentDataPath + "/MySaveData.dat") //in IF
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            //SaveData data = (SaveData)bf.Deserialize(file);
            //file.Close();

            playerDataStat.maxHealth = loadSave.maxHealth;
            playerDataStat.maxStamina = loadSave.maxStamina;
            playerDataStat.maxStaminaRegen = loadSave.maxStaminaRegen;
            playerDataStat.maxGuard = loadSave.maxGuard;
            playerDataStat.attackDamage = loadSave.attackDamage; 
            playerDataStat.speed = loadSave.speed;

            playerDataStat.item1Pocket = loadSave.item1Pocket;
            playerDataStat.item2Pocket = loadSave.item2Pocket;

            playerDataStat.hasSlasher = loadSave.hasSlasher;
            playerDataStat.hasSuperDuck = loadSave.hasSuperDuck;

            upgradeCost.upgradeHPCost = loadSave.upgradeHPCost;
            upgradeCost.upgradeSTMCost = loadSave.upgradeSTMCost;
            upgradeCost.upgradeSTMReCost = loadSave.upgradeSTMReCost;
            upgradeCost.upgradeGDCost = loadSave.upgradeGDCost;
            upgradeCost.upgradeDMGCost = loadSave.upgradeDMGCost;
            upgradeCost.upgradeSPDCost = loadSave.upgradeSPDCost;
            upgradeCost.upgradeSuperDuckCost = loadSave.upgradeSuperDuckCost;
            upgradeCost.upgradeSlasherCost = loadSave.upgradeSlasherCost;
            upgradeCost.upgradePocket1Cost = loadSave.upgradePocket1Cost;
            upgradeCost.upgradePocket2Cost = loadSave.upgradePocket2Cost;

            settingData.musicSound = loadSave.musicSound;
            settingData.ambientSound = loadSave.ambientSound;
            settingData.effectSound = loadSave.effectSound;
            settingData.uiSound = loadSave.uiSound;
            settingData.qualityIndex = loadSave.qualityIndex;
            settingData.isFullScreen = loadSave.isFullScreen;

            settingData.isTutorial = loadSave.isTutorial;
            settingData.notNewGame = loadSave.isTutorial;
            

            Debug.Log("Game data loaded!");
        }
        else
        {
            Debug.LogError("There is no save data!");
        }
    }

    public void LoadGameSetting()
    {
        string loadDataString = PlayerPrefs.GetString("Save");   
        SaveData loadSave = JsonUtility.FromJson<SaveData>(loadDataString);

        if (loadSave != null)
        {
            //File.Exists(Application.persistentDataPath + "/MySaveData.dat") //in IF
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            //SaveData data = (SaveData)bf.Deserialize(file);
            //file.Close();

            settingData.musicSound = loadSave.musicSound;
            settingData.ambientSound = loadSave.ambientSound;
            settingData.effectSound = loadSave.effectSound;
            settingData.uiSound = loadSave.uiSound;
            settingData.qualityIndex = loadSave.qualityIndex;
            settingData.isFullScreen = loadSave.isFullScreen;

            settingData.isTutorial = loadSave.isTutorial;
            settingData.notNewGame = loadSave.isTutorial;
            

            Debug.Log("Game data loaded!");
        }
        else
        {
            Debug.LogError("There is no save data!");
        }
    }
}

[Serializable]
public class SaveData
{
    [Header("Base Status")]
    public float maxHealth;
    public float maxStamina;
    public float maxStaminaRegen;
    public float maxGuard;
    public float attackDamage; 
    public float speed;

    [Header("Items")]
    public int item1Pocket;
    public int item2Pocket;

    [Header("Skill")]
    public bool hasSlasher;
    public bool hasSuperDuck;

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

    [Header("All Setting")]
    public float musicSound;
    public float ambientSound;
    public float effectSound;
    public float uiSound;
    public int qualityIndex;
    public bool isFullScreen;

    [Header("Menu Setting")]
    public bool isTutorial;
    public bool notNewGame;

}







