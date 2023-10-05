using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIMenuManager : MonoBehaviour
{
    [Header("Data")]
    public SettingData settingData;
    public StageData stageData;
    public BaseStatus playerDataStat;
    public UpgradeData upgradeCost;

    [Header("Panel")]
    public GameObject yNnPanel;
    public GameObject settingPanel;

    [Header("Button")]
    public GameObject continueBtn;

    void Start()
    {
        settingPanel.SetActive(false);
        yNnPanel.SetActive(false);
    }

    void Update()
    {
        continueBtn.SetActive(settingData.notNewGame);
    }

    public void NewGame()
    {
        if(!yNnPanel.activeSelf || !settingPanel.activeSelf) 
        {
            SceneManager.LoadScene("Lobby");
            //Reset Status Value
            playerDataStat.level = 1;
            playerDataStat.exp = 0;
            playerDataStat.maxExp = 1;
            playerDataStat.Point = 0;
            playerDataStat.currentPoint = 0;
            stageData.currentStage = 1;

            //Status
            playerDataStat.maxHealth = 100;
            playerDataStat.maxShield = 30;
            playerDataStat.attackDamage = 15; 
            playerDataStat.speed = 5;

            //Skill 
            playerDataStat.hasSlasher = false;
            playerDataStat.hasBarrier = false;
            playerDataStat.slasherLv = 1;
            playerDataStat.barrierLv = 1;

            //Upgrade Cost
            upgradeCost.upgradeHPCost = 25;
            upgradeCost.upgradeSHDCost = 20;
            upgradeCost.upgradeDMGCost = 35;
            upgradeCost.upgradeSPDCost = 30;
            upgradeCost.upgradeBarrierCost = 75;
            upgradeCost.upgradeSlasherCost = 100;
        }
        
    }

    public void Continue()
    {
        if(!yNnPanel.activeSelf || !settingPanel.activeSelf) SceneManager.LoadScene("Lobby");
        //Load Status Value
    }

    public void Setting()
    {
       if(!yNnPanel.activeSelf) settingPanel.SetActive(true);
    }

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
