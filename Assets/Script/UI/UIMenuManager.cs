using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIMenuManager : MonoBehaviour
{
    [Header("Data")]
    public SaveManager saveData;
    public SettingData settingData;
    public StageData stageData;
    public BaseStatus playerDataStat;
    public UpgradeData upgradeCost;

    [Header("Panel")]
    public GameObject BGPanel;
    public GameObject yNnPanel;
    public GameObject settingPanel;

    [Header("Button")]
    public GameObject continueBtn;

    [Header("FullScreen Toggle")]
    public Toggle fullScreenValue;

    [Header("Quality Dropdown")]
    public TMP_Dropdown qualityValue;

    [Header("Sound Slider")]
    public Slider musicSlider;
    public Slider effectSlider;
    public Slider ambientSlider;
    public Slider uiSlider;

    [Header("Audio Source")]
    public AudioSource musicSound;
    public AudioSource ambientSound;
    public AudioSource effectSound;
    public AudioSource uiSound;

    void Start()
    {
        settingPanel.SetActive(false);
        BGPanel.SetActive(false);
        yNnPanel.SetActive(false);

        SetAllSetting();
        saveData.LoadGameSetting();
    }

    void Update()
    {
        continueBtn.SetActive(settingData.notNewGame);
    }

    public void NewGame()
    {
        if(!yNnPanel.activeSelf || !settingPanel.activeSelf) 
        {
            if(settingData.isTutorial)
            {
                SceneManager.LoadScene("Tutorial");
            }
            else
            {
                SceneManager.LoadScene("Lobby");
            }
            
            //Reset Game Value
            playerDataStat.level = 1;
            playerDataStat.exp = 0;
            playerDataStat.maxExp = 1;
            playerDataStat.Eggs = 0;
            playerDataStat.currentEggs = 0;
            stageData.currentStage = 1;
            stageData.HighestStage = 0;

            //Status
            playerDataStat.maxHealth = playerDataStat.maxHealthStarter;
            playerDataStat.maxStamina = playerDataStat.maxStaminaStarter;
            playerDataStat.maxStaminaRegen = playerDataStat.maxStaminaRegenStarter;
            playerDataStat.maxGuard = playerDataStat.maxGuardStarter;
            playerDataStat.attackDamage = playerDataStat.attackDamageStarter; 
            playerDataStat.speed = playerDataStat.speedStarter;

            //Skill 
            playerDataStat.hasSlasher = false;
            playerDataStat.hasSuperDuck = false;
            playerDataStat.slasherLv = 1;
            playerDataStat.superDuckLv = 1;

            //Items Pocket
            playerDataStat.item1Pocket = 3;
            playerDataStat.item2Pocket = 3;

            //Upgrade Cost
            upgradeCost.upgradeHPCost = 25;
            upgradeCost.upgradeSTMCost = 25;
            upgradeCost.upgradeSTMReCost = 30;
            upgradeCost.upgradeGDCost = 20;
            upgradeCost.upgradeDMGCost = 35;
            upgradeCost.upgradeSPDCost = 30;
            upgradeCost.upgradeSuperDuckCost = 150;
            upgradeCost.upgradeSlasherCost = 100;
            upgradeCost.upgradePocket1Cost = 40;
            upgradeCost.upgradePocket2Cost = 40;

            settingData.notNewGame = true;

            saveData.SaveGame();
        }
        
    }

    public void Continue()
    {
        if(!yNnPanel.activeSelf || !settingPanel.activeSelf) 
            saveData.LoadGame();
            SceneManager.LoadScene("Lobby");
    }

    public void Setting()
    {
       if(!yNnPanel.activeSelf) 
            settingPanel.SetActive(true);
            BGPanel.SetActive(true);
    }

#region Quit Functions
    public void Quit()
    {
        saveData.SaveGame();
        Application.Quit();
    }

    public void notQuit()
    {
        yNnPanel.SetActive(false);
        BGPanel.SetActive(false);
    }

    public void YesNNoQuit()
    {
       if(!settingPanel.activeSelf) 
            yNnPanel.SetActive(true);
            BGPanel.SetActive(true);
    }
#endregion

#region Setting Functions

    public void FinishSetting()
    {
        //Save Setting Value
        saveData.SaveGame();
        settingPanel.SetActive(false);
        BGPanel.SetActive(false);
    }

    public void SetAllSetting()
    {
        //Sound
        uiSound.volume = settingData.uiSound;
        uiSlider.value = uiSound.volume;   

        effectSound.volume = settingData.effectSound;
        effectSlider.value = effectSound.volume; 

        musicSound.volume = settingData.musicSound;
        musicSlider.value = musicSound.volume; 

        ambientSound.volume = settingData.ambientSound;
        ambientSlider.value = ambientSound.volume; 

        //Display
        fullScreenValue.isOn = settingData.isFullScreen;

        //Quality
        qualityValue.value = settingData.qualityIndex;
    }

    public void MusicSetting()
    {
        settingData.musicSound = musicSlider.value;
        musicSound.volume = settingData.musicSound;
    }

    public void AmbientSetting()
    {
        settingData.ambientSound = ambientSlider.value;
        ambientSound.volume = settingData.ambientSound;
    }

    public void EffectSetting()
    {
        settingData.effectSound = effectSlider.value;
        effectSound.volume = settingData.effectSound;
    }

    public void UISetting()
    {
        settingData.uiSound = uiSlider.value;
        uiSound.volume = settingData.uiSound;
        
    }

    public void QualitySetting(int qualityIndex)
    {
        qualityIndex = qualityValue.value;
        settingData.qualityIndex = qualityValue.value;
        QualitySettings.SetQualityLevel(qualityIndex);  
    }

    public void FullScreenSetting(bool isFullScreen)
    {
        isFullScreen = fullScreenValue.isOn;
        settingData.isFullScreen = fullScreenValue.isOn;
        Screen.fullScreen = isFullScreen;   
    }
#endregion

}
