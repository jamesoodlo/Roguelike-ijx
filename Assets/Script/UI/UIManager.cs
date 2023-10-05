using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    UIInput uiInput;
    private bool SkillUpPhase = false;
    
    [Header("Data")]
    public StageData stageData;
    public BaseStatus playerDataStat;

    [Header("Panel")]
    public GameObject pausedPanel;
    public GameObject nextStagePanel;
    public GameObject upgradePanel;
    public GameObject settingPanel;
    public GameObject yNnQuitPanel;
    public GameObject yNnLobbyPanel;
    public GameObject yNnNextStagePanel;

    [Header("Button")]
    public GameObject SlasherLvUpBtn;
    public GameObject BarrierLvUpBtn;

    [Header("Text")]
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI pointText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI slasherLvText;
    public TextMeshProUGUI barrierLvText;

    [Header("Slider")]
    public Slider healthBar;
    public Slider shieldBar;
    public Slider ExpBar;
    public Slider[] cdBar;

    void Start()
    {
        uiInput = GetComponent<UIInput>();

        yNnQuitPanel.SetActive(false);
        yNnLobbyPanel.SetActive(false);
        yNnNextStagePanel.SetActive(false);
        pausedPanel.SetActive(false);
        settingPanel.SetActive(false);

        healthBar.maxValue = playerDataStat.maxHealth;
        shieldBar.maxValue = playerDataStat.maxShield;
    }

    void Update()
    {
        upgradePanel.SetActive(SkillUpPhase);
        SlasherLvUpBtn.SetActive(playerDataStat.hasSlasher);
        BarrierLvUpBtn.SetActive(playerDataStat.hasBarrier);
        SetDataToText();
        SetStatusBar();
        SetSkill();
        LvUp();

        if(uiInput.escape)
        {
            Time.timeScale = 0;
            pausedPanel.SetActive(true);
        }
    }

    public void SetDataToText()
    {
        stageText.text = stageData.currentStage.ToString();
        pointText.text = playerDataStat.currentPoint.ToString();
        levelText.text = playerDataStat.level.ToString();
        slasherLvText.text = playerDataStat.slasherLv.ToString();
        barrierLvText.text = playerDataStat.barrierLv.ToString();
    }

    public void LvUp()
    {
        bool isLvUP = false;

        if(playerDataStat.level  % 5 == 0 && playerDataStat.canLvUp)
        {
            isLvUP = true;
        }

        if(isLvUP)
        {
            if(playerDataStat.hasBarrier || playerDataStat.hasSlasher) SkillUpPhase = true;
            isLvUP = false;
        }

        if(SkillUpPhase)
        {
            Time.timeScale = 0;
        }
        else
        {
            
        }
    }

    public void SetStatusBar()
    {
        PlayerStats stats = FindObjectOfType<PlayerStats>();
        healthBar.value = stats.currentHealth;
        shieldBar.value = stats.currentShield;
        
        ExpBar.value = playerDataStat.exp;
        ExpBar.maxValue = playerDataStat.maxExp;
    }

    public void SetSkill()
    {
        PlayerController playerControll = FindObjectOfType<PlayerController>();

        cdBar[0].maxValue = playerControll.maxSlashCdTime;
        cdBar[0].value = playerControll.slashCdTime;

        cdBar[1].maxValue = playerControll.maxBarrierCdTime;
        cdBar[1].value = playerControll.barrierCdTime;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pausedPanel.SetActive(false);
    }

    public void Setting()
    {
        pausedPanel.SetActive(false);
        settingPanel.SetActive(true);
    }

    public void Paused()
    {
        settingPanel.SetActive(false);
        pausedPanel.SetActive(true);
    }

#region Skill Lv Up Functions

    public void SlasherLvUp()
    {
        if(playerDataStat.hasSlasher)
        {
            if(playerDataStat.slasherLv > 10) playerDataStat.slasherLv = 10;
            playerDataStat.slasherLv += 1;
            playerDataStat.canLvUp = false;
            SkillUpPhase = false;
            Time.timeScale = 1;
        }
    }

    public void BarrierLvUp()
    {
        if(playerDataStat.hasBarrier)
        {
            if(playerDataStat.barrierLv > 10) playerDataStat.barrierLv = 10;
            playerDataStat.barrierLv += 1;
            playerDataStat.canLvUp = false;
            SkillUpPhase = false;
            Time.timeScale = 1;
        }
    }

#endregion 

#region Lobby Functions

    public void YesNNoLobby()
    {
        yNnLobbyPanel.SetActive(true);
        pausedPanel.SetActive(false);
    }

    public void Lobby()
    {
        playerDataStat.level = 1;
        playerDataStat.exp = 0;
        playerDataStat.maxExp = 1;
        stageData.currentStage = 1;
        playerDataStat.currentPoint = 0;
        playerDataStat.slasherLv = 1;
        playerDataStat.barrierLv = 1;
        Time.timeScale = 1;
        SceneManager.LoadScene("Lobby");
    }

    public void notLobby()
    {
        yNnLobbyPanel.SetActive(false);
        pausedPanel.SetActive(true);
    }

#endregion

#region Quit Functions

    public void YesNNoQuit()
    {
        yNnQuitPanel.SetActive(true);
    }

    public void Quit()
    {
        playerDataStat.level = 1;
        playerDataStat.exp = 0;
        playerDataStat.maxExp = 1;
        stageData.currentStage = 1;
        playerDataStat.currentPoint = 0;
        playerDataStat.slasherLv = 1;
        playerDataStat.barrierLv = 1;
        Time.timeScale = 1;
        Application.Quit();
    }

    public void notQuit()
    {
        yNnQuitPanel.SetActive(false);
        pausedPanel.SetActive(true);
    }

#endregion

#region Next Stage Functions
    public void GoToUpgrade()
    {
        yNnNextStagePanel.SetActive(true);
        nextStagePanel.SetActive(false);
    }

    public void YesGoUpgrade()
    {
        playerDataStat.Point += playerDataStat.currentPoint;
        playerDataStat.Point -= playerDataStat.Point * 45 / 100;
        SceneManager.LoadScene("Lobby");
        playerDataStat.currentPoint = 0;
        Time.timeScale = 1;
    }

    public void NoGoBack()
    {
        yNnNextStagePanel.SetActive(false);
        nextStagePanel.SetActive(true);
    }

    public void NextStage()
    {
        Time.timeScale = 1;

        if(stageData.currentStage == 1 && stageData.currentStage < 2)
        {
            stageData.currentStage = 2;
            SceneManager.LoadScene("Stage");
        }
        else if(stageData.currentStage == 2 && stageData.currentStage < 3)
        {
            stageData.currentStage = 3;
            SceneManager.LoadScene("Stage");
        }
        else if(stageData.currentStage == 3 && stageData.currentStage < 4)
        {
            stageData.currentStage = 4;
            SceneManager.LoadScene("Stage");
        }
        else if(stageData.currentStage == 4 && stageData.currentStage < 5)
        {
            stageData.currentStage = 5;
            SceneManager.LoadScene("Stage");
        }
        else if(stageData.currentStage == 5 && stageData.currentStage < 6)
        {
            stageData.currentStage = 6;
            SceneManager.LoadScene("Stage");
        }
        else if(stageData.currentStage == 6 && stageData.currentStage < 7)
        {
            stageData.currentStage = 7;
            SceneManager.LoadScene("Stage");
        }
        else if(stageData.currentStage == 7 && stageData.currentStage < 8)
        {
            stageData.currentStage = 8;
            SceneManager.LoadScene("Stage");
        }
        else if(stageData.currentStage == 8 && stageData.currentStage < 9)
        {
            stageData.currentStage = 9;
            SceneManager.LoadScene("Stage");
        } 
        else if(stageData.currentStage == 9 && stageData.currentStage < 10)
        {
            stageData.currentStage = 10;
            SceneManager.LoadScene("Stage");
        }
        else if(stageData.currentStage == 10 && stageData.currentStage < 11)
        {
            stageData.currentStage = 11;
            SceneManager.LoadScene("Stage");
        }
        else if(stageData.currentStage == 11 && stageData.currentStage < 12)
        {
            stageData.currentStage = 12;
            SceneManager.LoadScene("Stage");
        }
        else if(stageData.currentStage == 12 && stageData.currentStage < 13)
        {
            stageData.currentStage = 13;
            SceneManager.LoadScene("Stage");
        }
        else if(stageData.currentStage == 13 && stageData.currentStage < 14)
        {
            stageData.currentStage = 14;
            SceneManager.LoadScene("Stage");
        }
        else if(stageData.currentStage == 14 && stageData.currentStage < 15)
        {
            stageData.currentStage = 15;
            SceneManager.LoadScene("Stage");
        }
        else if(stageData.currentStage > 15)
        {
            stageData.currentStage = 1;
            Time.timeScale = 0;
            SceneManager.LoadScene("Lobby");
        }
    }
#endregion 

#region Setting Functions

    public void FinishSetting()
    {
        //Save Setting Value
        settingPanel.SetActive(false);
        pausedPanel.SetActive(true);
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
