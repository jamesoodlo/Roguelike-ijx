using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UILobbyManager : MonoBehaviour
{
    [Header("Data")]
    public SaveManager saveData;
    public BaseStatus playerDataStat;
    public UpgradeData upgradeData;
    public StageData stageData;
    public SettingData settingData;

    [Header("Panel")]
    public GameObject yNnPanel;
    public GameObject settingPanel;
    public GameObject settingPanelBG;
    public GameObject upgradePanelBG;

    [Header("Eggs")]
    public TextMeshProUGUI Eggs;

    [Header("Stage")]
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI highestStageText;

    [Header("Health")]
    public Slider healthBar;
    public TextMeshProUGUI HPCostText;

    [Header("Stamina")]
    public Slider staminaBar;
    public TextMeshProUGUI STMCostText;

    [Header("Stamina Regen")]
    public Slider staminaRegenBar;
    public TextMeshProUGUI STMReCostText;

    [Header("Guard")]
    public Slider guardBar;
    public TextMeshProUGUI GDCostText;

    [Header("Damage")]
    public Slider damageBar;
    public TextMeshProUGUI DMGCostText;

    [Header("Speed")]
    public Slider speedBar;
    public TextMeshProUGUI SPDCostText;

    [Header("Pocket Items")]
    public TextMeshProUGUI Pocket1Text;
    public TextMeshProUGUI Pocket2Text;
    public TextMeshProUGUI Pocket1CostText;
    public TextMeshProUGUI Pocket2CostText;

    [Header("Skill")]
    public TextMeshProUGUI SuperDuckCostText;
    public TextMeshProUGUI SlasherCostText;

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

    [Header("FullScreen Toggle")]
    public Toggle fullScreenValue;

    [Header("Quality Dropdown")]
    public TMP_Dropdown qualityValue;

    void Start()
    {
        settingPanel.SetActive(false);
        settingPanelBG.SetActive(false);
        upgradePanelBG.SetActive(false);
        yNnPanel.SetActive(false);

        Time.timeScale = 1;

        SetAllSetting();

        if(playerDataStat.Eggs != 0 || playerDataStat.currentEggs != 0) 
            playerDataStat.Eggs += playerDataStat.currentEggs;
            playerDataStat.currentEggs = 0;
    }

    void Update()
    {
        SetStatBar();
        SetDataToText();
        SetUpgradeCost();
    }

    public void SetStatBar()
    {
        healthBar.value = playerDataStat.maxHealth;
        staminaBar.value = playerDataStat.maxStamina;
        staminaRegenBar.value = playerDataStat.maxStaminaRegen;
        guardBar.value = playerDataStat.maxGuard;
        damageBar.value = playerDataStat.attackDamage;
        speedBar.value = playerDataStat.speed;
    }

    private void SetUpgradeCost()
    {
        UpdateUpgradeCostText(HPCostText, healthBar.value, healthBar.maxValue, upgradeData.upgradeHPCost);
        UpdateUpgradeCostText(STMCostText, staminaBar.value, staminaBar.maxValue, upgradeData.upgradeSTMCost);
        UpdateUpgradeCostText(STMReCostText, staminaRegenBar.value, staminaRegenBar.maxValue, upgradeData.upgradeSTMReCost);
        UpdateUpgradeCostText(GDCostText, guardBar.value, guardBar.maxValue, upgradeData.upgradeGDCost);
        UpdateUpgradeCostText(DMGCostText, damageBar.value, damageBar.maxValue, upgradeData.upgradeDMGCost);
        UpdateUpgradeCostText(SPDCostText, speedBar.value, speedBar.maxValue, upgradeData.upgradeSPDCost);
        
        Pocket1Text.text = playerDataStat.item1Pocket.ToString();
        Pocket2Text.text = playerDataStat.item2Pocket.ToString();
        Pocket1CostText.text = upgradeData.upgradePocket1Cost.ToString();
        Pocket2CostText.text = upgradeData.upgradePocket2Cost.ToString();
    }

    private void UpdateUpgradeCostText(TMPro.TextMeshProUGUI costText, float value, float maxValue, int upgradeCost)
    {
        costText.text = (value < maxValue) ? upgradeCost.ToString() : "Max";
    }

    public void SetDataToText()
    {

        Eggs.text = playerDataStat.Eggs.ToString();
        stageText.text = stageData.currentStage.ToString();
        highestStageText.text = stageData.HighestStage.ToString();
        
        //Skill Unlock
        if(!playerDataStat.hasSuperDuck) 
        {
            SuperDuckCostText.text = upgradeData.upgradeSuperDuckCost.ToString();
        }
        else
        {
            SuperDuckCostText.text = " ";
        }

        if(!playerDataStat.hasSlasher) 
        {
            SlasherCostText.text = upgradeData.upgradeSlasherCost.ToString();
        }
        else
        {
            SlasherCostText.text = " ";
        }
    }

    public void StartGame()
    {
        if(yNnPanel.activeSelf || settingPanel.activeSelf) 
        {

        }
        else
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Stage");
        }
    }

    public void MainMenu()
    {
        if(yNnPanel.activeSelf || settingPanel.activeSelf) 
        {

        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void Setting()
    {
       if(!yNnPanel.activeSelf) 
            settingPanel.SetActive(true);
            settingPanelBG.SetActive(true);
    }

#region Upgrade Functions

    public void HealthUpgrade()
    {
        if(yNnPanel.activeSelf || settingPanel.activeSelf)
        {
            
        }
        else
        {
            if(playerDataStat.Eggs >= upgradeData.upgradeHPCost && healthBar.value < healthBar.maxValue)
            {
                playerDataStat.Eggs -= upgradeData.upgradeHPCost;
                playerDataStat.maxHealth += 25;
                upgradeData.upgradeHPCost += 25;
                saveData.SaveGame();
            }
            else
            {

            }
        }
    }

    public void StaminaUpgrade()
    {
        if(yNnPanel.activeSelf || settingPanel.activeSelf)
        {
            
        }
        else
        {
            if(playerDataStat.Eggs >= upgradeData.upgradeSTMCost && staminaBar.value < staminaBar.maxValue)
            {
                playerDataStat.Eggs -= upgradeData.upgradeSTMCost;
                playerDataStat.maxStamina += 20;
                upgradeData.upgradeSTMCost += 25;
                saveData.SaveGame();
            }
            else
            {

            }
        }
    }

    public void StaminaRegenUpgrade()
    {
        if(yNnPanel.activeSelf || settingPanel.activeSelf)
        {
            
        }
        else
        {
            if(playerDataStat.Eggs >= upgradeData.upgradeSTMReCost && staminaRegenBar.value < staminaRegenBar.maxValue)
            {
                playerDataStat.Eggs -= upgradeData.upgradeSTMReCost;
                playerDataStat.maxStaminaRegen += 5;
                upgradeData.upgradeSTMReCost += 30;
                saveData.SaveGame();
            }
            else
            {

            }
        }
    }

    public void GuardUpgrade()
    {
        if(yNnPanel.activeSelf || settingPanel.activeSelf)
        {
            
        }
        else
        {
            if(playerDataStat.Eggs >= upgradeData.upgradeGDCost && guardBar.value < guardBar.maxValue)
            {
                playerDataStat.Eggs -= upgradeData.upgradeGDCost;
                playerDataStat.maxGuard += 15;
                upgradeData.upgradeGDCost += 20;
                saveData.SaveGame();
            }
            else
            {
                
            }
        }
    }

    public void DamageUpgrade()
    {
        if(yNnPanel.activeSelf || settingPanel.activeSelf)
        {
            
        }
        else
        {
            if(playerDataStat.Eggs >= upgradeData.upgradeDMGCost && damageBar.value < damageBar.maxValue)
            {
                playerDataStat.Eggs -= upgradeData.upgradeDMGCost;
                playerDataStat.attackDamage += 5;
                upgradeData.upgradeDMGCost += 30;
                saveData.SaveGame();
            }
            else
            {
                
            }
        }
    }

    public void SpeedUpgrade()
    {
        if(yNnPanel.activeSelf || settingPanel.activeSelf)
        {
            
        }
        else
        {
            if(playerDataStat.Eggs >= upgradeData.upgradeSPDCost && speedBar.value < speedBar.maxValue)
            {
                playerDataStat.Eggs -= upgradeData.upgradeSPDCost;
                playerDataStat.speed += 1;
                upgradeData.upgradeSPDCost += 35;
                saveData.SaveGame();
            }
            else
            {
                
            }
        }
    }

    public void UnlockSkillQ()
    {
        if(yNnPanel.activeSelf || settingPanel.activeSelf)
        {
            
        }
        else
        {
            if(playerDataStat.Eggs >= upgradeData.upgradeSlasherCost && !playerDataStat.hasSlasher)
            {
                playerDataStat.Eggs -= upgradeData.upgradeSlasherCost;
                playerDataStat.hasSlasher = true;
                saveData.SaveGame();
            }
            else
            {
                
            }
        }
    }

    public void UnlockSkillE()
    {
        if(yNnPanel.activeSelf || settingPanel.activeSelf)
        {
            
        }
        else
        {
            if(playerDataStat.Eggs >= upgradeData.upgradeSuperDuckCost && !playerDataStat.hasSuperDuck)
            {
                playerDataStat.Eggs -= upgradeData.upgradeSuperDuckCost;
                playerDataStat.hasSuperDuck = true;
                saveData.SaveGame();
            }
            else
            {
                
            }
        }
    }

    public void Pocket1Upgrade()
    {
        if(yNnPanel.activeSelf || settingPanel.activeSelf)
        {
            
        }
        else
        {
            if(playerDataStat.Eggs >= upgradeData.upgradePocket1Cost && playerDataStat.item1Pocket < 10)
            {
                playerDataStat.Eggs -= upgradeData.upgradePocket1Cost;
                playerDataStat.item1Pocket += 1;
                upgradeData.upgradePocket1Cost += 40;
                saveData.SaveGame();
            }
            else
            {
                
            }
        }
    }

    public void Pocket2Upgrade()
    {
        if(yNnPanel.activeSelf || settingPanel.activeSelf)
        {
            
        }
        else
        {
            if(playerDataStat.Eggs >= upgradeData.upgradePocket2Cost && playerDataStat.item2Pocket < 10)
            {
                playerDataStat.Eggs -= upgradeData.upgradePocket2Cost;
                playerDataStat.item2Pocket += 1;
                upgradeData.upgradePocket2Cost += 40;
                saveData.SaveGame();
            }
            else
            {
                
            }
        }
    }

    public void nextPanelUpgrade()
    {
        if(yNnPanel.activeSelf || settingPanel.activeSelf)
        {
            
        }
        else
        {
            upgradePanelBG.SetActive(true);
        }
        
    }

    public void backPanelUpgrade()
    {
        if(yNnPanel.activeSelf || settingPanel.activeSelf)
        {
            
        }
        else
        {
            upgradePanelBG.SetActive(false);
        }
    }
#endregion

#region Quit Functions
    public void Quit()
    {
        saveData.SaveGame();
        Application.Quit();
    }

    public void notQuit()
    {
        yNnPanel.SetActive(false);
    }

    public void YesNNoQuit()
    {
        if(!settingPanel.activeSelf) yNnPanel.SetActive(true);
    }
#endregion

#region Setting Functions

    public void FinishSetting()
    {
        //Save Setting Value
        saveData.SaveGame();
        settingPanel.SetActive(false);
        settingPanelBG.SetActive(false);
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
        QualitySettings.SetQualityLevel(qualityIndex, false);  
    }

    public void FullScreenSetting(bool isFullScreen)
    {
        isFullScreen = fullScreenValue.isOn;
        settingData.isFullScreen = fullScreenValue.isOn;
        Screen.fullScreen = isFullScreen;   
    }
#endregion


}
