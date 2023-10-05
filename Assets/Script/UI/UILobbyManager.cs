using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UILobbyManager : MonoBehaviour
{
    [Header("Data")]
    public BaseStatus playerDataStat;
    public UpgradeData upgradeData;
    public StageData stageData;

    [Header("Panel")]
    public GameObject yNnPanel;
    public GameObject settingPanel;

    [Header("Point")]
    public TextMeshProUGUI point;

    [Header("Stage")]
    public TextMeshProUGUI stageText;

    [Header("Health")]
    public Slider healthBar;
    public TextMeshProUGUI HPCostText;

    [Header("Shield")]
    public Slider shieldBar;
    public TextMeshProUGUI SHDCostText;

    [Header("Damage")]
    public Slider damageBar;
    public TextMeshProUGUI DMGCostText;

    [Header("Speed")]
    public Slider speedBar;
    public TextMeshProUGUI SPDCostText;

    [Header("Skill")]
    public TextMeshProUGUI BarrierCostText;
    public TextMeshProUGUI SlasherCostText;


    void Start()
    {
        settingPanel.SetActive(false);
        yNnPanel.SetActive(false);

        if(playerDataStat.Point != 0 || playerDataStat.currentPoint != 0) 
            playerDataStat.Point += playerDataStat.currentPoint;
            playerDataStat.currentPoint = 0;
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
        shieldBar.value = playerDataStat.maxShield;
        damageBar.value = playerDataStat.attackDamage;
        speedBar.value = playerDataStat.speed;
    }

    private void SetUpgradeCost()
    {
        UpdateUpgradeCostText(HPCostText, healthBar.value, healthBar.maxValue, upgradeData.upgradeHPCost);
        UpdateUpgradeCostText(SHDCostText, shieldBar.value, shieldBar.maxValue, upgradeData.upgradeSHDCost);
        UpdateUpgradeCostText(DMGCostText, damageBar.value, damageBar.maxValue, upgradeData.upgradeDMGCost);
        UpdateUpgradeCostText(SPDCostText, speedBar.value, speedBar.maxValue, upgradeData.upgradeSPDCost);
    }

    private void UpdateUpgradeCostText(TMPro.TextMeshProUGUI costText, float value, float maxValue, int upgradeCost)
    {
        costText.text = (value < maxValue) ? upgradeCost.ToString() : "Max";
    }

    public void SetDataToText()
    {

        point.text = playerDataStat.Point.ToString();
        stageText.text = stageData.currentStage.ToString();
        
        //Skill Unlock
        if(!playerDataStat.hasBarrier) 
        {
            BarrierCostText.text = upgradeData.upgradeBarrierCost.ToString();
        }
        else
        {
            BarrierCostText.text = "haveSkill";
        }

        if(!playerDataStat.hasSlasher) 
        {
            SlasherCostText.text = upgradeData.upgradeSlasherCost.ToString();
        }
        else
        {
            SlasherCostText.text = "haveSkill";
        }
    }

    public void StartGame()
    {
        if(yNnPanel.activeSelf || settingPanel.activeSelf) 
        {

        }
        else
        {
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
       if(!yNnPanel.activeSelf) settingPanel.SetActive(true);
    }

#region Upgrade Functions

    public void HealthUpgrade()
    {
        if(yNnPanel.activeSelf || settingPanel.activeSelf)
        {
            
        }
        else
        {
            if(playerDataStat.Point >= upgradeData.upgradeHPCost && healthBar.value < healthBar.maxValue)
            {
                playerDataStat.Point -= upgradeData.upgradeHPCost;
                playerDataStat.maxHealth += 25;
                upgradeData.upgradeHPCost += 25;
            }
            else
            {

            }
        }
    }

    public void ShieldUpgrade()
    {
        if(yNnPanel.activeSelf || settingPanel.activeSelf)
        {
            
        }
        else
        {
            if(playerDataStat.Point >= upgradeData.upgradeSHDCost && shieldBar.value < shieldBar.maxValue)
            {
                playerDataStat.Point -= upgradeData.upgradeSHDCost;
                playerDataStat.maxShield += 30;
                upgradeData.upgradeSHDCost += 20;
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
            if(playerDataStat.Point >= upgradeData.upgradeDMGCost && damageBar.value < damageBar.maxValue)
            {
                playerDataStat.Point -= upgradeData.upgradeDMGCost;
                playerDataStat.attackDamage += 5;
                upgradeData.upgradeDMGCost += 30;
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
            if(playerDataStat.Point >= upgradeData.upgradeSPDCost && speedBar.value < speedBar.maxValue)
            {
                playerDataStat.Point -= upgradeData.upgradeSPDCost;
                playerDataStat.speed += 1;
                upgradeData.upgradeSPDCost += 35;
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
            if(playerDataStat.Point >= upgradeData.upgradeSlasherCost && !playerDataStat.hasSlasher)
            {
                playerDataStat.Point -= upgradeData.upgradeSlasherCost;
                playerDataStat.hasSlasher = true;
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
            if(playerDataStat.Point >= upgradeData.upgradeBarrierCost && !playerDataStat.hasBarrier)
            {
                playerDataStat.Point -= upgradeData.upgradeBarrierCost;
                playerDataStat.hasBarrier = true;
            }
            else
            {
                
            }
        }
    }
#endregion

#region Quit Functions
    public void Quit()
    {
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
        settingPanel.SetActive(false);
    }

    public void MusicSetting()
    {
        //Set Music Sound
    }

    public void AmbientSetting()
    {
        //Set Ambient Sound
    }

    public void EffectSetting()
    {
        //Set Effect Sound
    }

    public void QualitySettings(int qualityIndex)
    {
        //qualityIndex = graphicValue.value;
        //settingData.qualityIndex = graphicValue.value;
        //QualitySettings.SetQualityLevel(qualityIndex);  
    }

    public void FullScreenSetting(bool isFullScreen)
    {
        //isFullScreen = fullScreenValue.isOn;
        //settingData.isFullScreen = fullScreenValue.isOn;
        //Screen.fullScreen = isFullScreen;   
    }
#endregion


}
