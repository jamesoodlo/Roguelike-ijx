using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    UIInput uiInput;
    private PlayerController playerControll;
    private PlayerStats stats;
    private Inventory inven;
    private bool SkillUpPhase = false;
    
    [Header("Data")]
    public SaveManager saveData;
    public StageData stageData;
    public BaseStatus playerDataStat;
    public SettingData settingData;

    [Header("Panel")]
    public GameObject skillECdPanel;
    public GameObject skillQCdPanel;
    public GameObject BGPanel;
    public GameObject pausedPanel;
    public GameObject nextStagePanel;
    public GameObject upgradePanel;
    public GameObject settingPanel;
    public GameObject yNnQuitPanel;
    public GameObject yNnLobbyPanel;
    public GameObject yNnNextStagePanel;
    public GameObject gameOverPanel;

    [Header("Skill")]
    public GameObject SlasherImg;
    public GameObject SuperDuckImg;
    public GameObject SlasherLvUpBtn;
    public GameObject SuperDuckLvUpBtn;

    [Header("Text")]
    public GameObject gameOverText;
    public GameObject lastStageText;
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI HighestStageText;
    public TextMeshProUGUI EggsText;
    public TextMeshProUGUI EggsGameOverText;
    public TextMeshProUGUI item1Num;
    public TextMeshProUGUI item2Num;
    public TextMeshProUGUI item2Time;
    public TextMeshProUGUI item1Pocket;
    public TextMeshProUGUI item2Pocket;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI slasherLvText;
    public TextMeshProUGUI superDuckLvText;
    public TextMeshProUGUI slasherTimeText;
    public TextMeshProUGUI superDuckTimeText;
    public TextMeshProUGUI slasherCdTimeText;
    public TextMeshProUGUI superDuckCdTimeText;

    [Header("Slider")]
    public Slider healthBar;
    public Slider guardBar;
    public Slider staminaBar;
    public Slider ExpBar;

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
        uiInput = GetComponent<UIInput>();
        playerControll = FindObjectOfType<PlayerController>();
        stats = FindObjectOfType<PlayerStats>();
        inven = FindObjectOfType<Inventory>();

        yNnQuitPanel.SetActive(false);
        yNnLobbyPanel.SetActive(false);
        yNnNextStagePanel.SetActive(false);
        pausedPanel.SetActive(false);
        settingPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        BGPanel.SetActive(false);

        SetAllSetting();
    }

    void Update()
    {
        healthBar.maxValue = playerDataStat.maxHealth;
        guardBar.maxValue = playerDataStat.maxGuard;
        staminaBar.maxValue = playerDataStat.maxStamina;

        upgradePanel.SetActive(SkillUpPhase);
        SlasherLvUpBtn.SetActive(playerDataStat.hasSlasher);
        SuperDuckLvUpBtn.SetActive(playerDataStat.hasSuperDuck);
        SlasherImg.SetActive(playerDataStat.hasSlasher);
        SuperDuckImg.SetActive(playerDataStat.hasSuperDuck);
        
        SetDataToText();
        SetStatusBar();
        SetSkill();
        LvUp();

        if(uiInput.escape)
        {
            Time.timeScale = 0;
            pausedPanel.SetActive(true);
            BGPanel.SetActive(true);
        }
    }

    public void SetDataToText()
    {
        stageText.text = stageData.currentStage.ToString();
        HighestStageText.text = stageData.HighestStage.ToString();
        EggsText.text = playerDataStat.currentEggs.ToString();
        item1Num.text = playerDataStat.item1Num.ToString();
        item2Num.text = playerDataStat.item2Num.ToString();
        item1Pocket.text = playerDataStat.item1Pocket.ToString();
        item2Pocket.text = playerDataStat.item2Pocket.ToString();
        EggsGameOverText.text = playerDataStat.currentEggs.ToString(); 
        levelText.text = playerDataStat.level.ToString();
        slasherLvText.text = playerDataStat.slasherLv.ToString();
        superDuckLvText.text = playerDataStat.superDuckLv.ToString();

        

        if(inven.isBarrier)
        {
            item2Time.text = inven.barrierTime.ToString("F1");
        } 
        else
        {
            item2Time.text = " ";
        }
    }

    public void LvUp()
    {
        bool isLvUP = false;

        if(playerDataStat.level % 5 == 0 && playerDataStat.canLvUp && playerDataStat.slasherLv < 10 && playerDataStat.superDuckLv < 10)
        {
            isLvUP = true;
        }

        if(isLvUP)
        {
            if(playerDataStat.hasSuperDuck || playerDataStat.hasSlasher) SkillUpPhase = true;
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
        healthBar.value = stats.currentHealth;
        guardBar.value = stats.currentGuard;
        staminaBar.value = stats.currentStamina;

        
        ExpBar.value = playerDataStat.exp;
        ExpBar.maxValue = playerDataStat.maxExp;
    }

    public void SetSkill()
    {
        if(playerControll.isSlash)
        {
            slasherTimeText.text = playerControll.slashTime.ToString("F1");
        } 
        else
        {
            slasherTimeText.text = " ";
        }

        if(playerControll.isSuperDuck) 
        {
            superDuckTimeText.text = playerControll.superDuckTime.ToString("F1");
            
        }
        else
        {
            superDuckTimeText.text = " ";
        }

        if(playerControll.canSlash)
        {
            slasherCdTimeText.text = " ";
            skillQCdPanel.SetActive(false);
        }
        else
        {
            slasherCdTimeText.text = playerControll.slashCdTime.ToString("F1");
            skillQCdPanel.SetActive(true);
        }

        if(playerControll.canSuperDuck)
        {
            superDuckCdTimeText.text = " ";
            skillECdPanel.SetActive(false);
        }
        else
        {
            superDuckCdTimeText.text = playerControll.superDuckCdTime.ToString("F1");
            skillECdPanel.SetActive(true);
        }
        
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pausedPanel.SetActive(false);
        BGPanel.SetActive(false);
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

    public void superDuckLvUp()
    {
        if(playerDataStat.hasSuperDuck)
        {
            if(playerDataStat.superDuckLv > 10) playerDataStat.superDuckLv = 10;
            playerDataStat.superDuckLv += 1;
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
        playerDataStat.currentEggs = 0;
        playerDataStat.slasherLv = 1;
        playerDataStat.superDuckLv = 1;
        playerDataStat.item1Num = 0;
        playerDataStat.item2Num = 0;
        Time.timeScale = 1;
        playerControll.isSlash = false;
        playerDataStat.maxHealth = playerDataStat.currentMaxHealth;
        playerDataStat.attackDamage = playerDataStat.currentAttackDamage;
        playerDataStat.speed = playerDataStat.currentSpeed;
        saveData.SaveGame();
        SceneManager.LoadScene("Lobby");
    }

    public void notLobby()
    {
        yNnLobbyPanel.SetActive(false);
        pausedPanel.SetActive(true);
    }

    public void LobbyGameOVer()
    {
        playerDataStat.Eggs += playerDataStat.currentEggs;
        Time.timeScale = 1;
        saveData.SaveGame();
        Lobby();
    }

#endregion

#region Quit Functions

    public void YesNNoQuit()
    {
        yNnQuitPanel.SetActive(true);
        pausedPanel.SetActive(false);
    }

    public void Quit()
    {
        playerDataStat.level = 1;
        playerDataStat.exp = 0;
        playerDataStat.maxExp = 1;
        stageData.currentStage = 1;
        playerDataStat.currentEggs = 0;
        playerDataStat.slasherLv = 1;
        playerDataStat.superDuckLv = 1;
        playerDataStat.item1Num = 0;
        playerDataStat.item2Num = 0;
        playerControll.isSlash = false;
        playerDataStat.maxHealth = playerDataStat.currentMaxHealth;
        playerDataStat.attackDamage = playerDataStat.currentAttackDamage;
        playerDataStat.speed = playerDataStat.currentSpeed;
        Time.timeScale = 1;
        saveData.SaveGame();
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
        playerDataStat.Eggs += playerDataStat.currentEggs;
        playerDataStat.Eggs -= playerDataStat.Eggs * 45 / 100;
        playerControll.isSlash = false;
        playerDataStat.maxHealth = playerDataStat.currentMaxHealth;
        playerDataStat.attackDamage = playerDataStat.currentAttackDamage;
        playerDataStat.speed = playerDataStat.currentSpeed;
        playerDataStat.currentEggs = 0;
        stageData.currentStage += 1;
        Time.timeScale = 1;
        saveData.SaveGame();
        SceneManager.LoadScene("Lobby");
    }

    public void NoGoBack()
    {
        yNnNextStagePanel.SetActive(false);
        nextStagePanel.SetActive(true);
    }

    public void NextStage()
    {
        Time.timeScale = 1;

        if(stageData.currentStage < stageData.maxStage)
        {
            bool nextStage = true;

            if(nextStage)
                nextStage = false;
                playerControll.isSlash = false;
                playerDataStat.maxHealth = playerDataStat.currentMaxHealth;
                playerDataStat.attackDamage = playerDataStat.currentAttackDamage;
                playerDataStat.speed = playerDataStat.currentSpeed;
                stageData.currentStage++;
                SceneManager.LoadScene("Stage");
        }
        else if(stageData.currentStage >= stageData.maxStage)
        {

        }
        
    }
#endregion 

#region Setting Functions

    public void FinishSetting()
    {
        //Save Setting Value
        saveData.SaveGame();
        settingPanel.SetActive(false);
        pausedPanel.SetActive(true);
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
