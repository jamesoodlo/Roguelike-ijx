using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UITutorialManager : MonoBehaviour
{
    UIInput uiInput;

    public SaveManager saveData;

    public SettingData settingData;

    public int numOfpage;

    public GameObject[] tutorialPanel;

    [Header("Panel")]
    public GameObject settingPanel;
    public GameObject BGPanel;

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
        numOfpage = 0;

        uiInput = GetComponent<UIInput>();

        SetAllSetting();

        settingPanel.SetActive(false);
        BGPanel.SetActive(false);
    }

    void Update()
    {
        tutorialPanel[numOfpage].SetActive(true);

        if(uiInput.escape)
        {
            Time.timeScale = 0;
            settingPanel.SetActive(true);
            BGPanel.SetActive(true);
        }
    }

    public void nextPanel()
    {
        if(numOfpage < 7) numOfpage++;
    }

    public void nextPlay()
    {
        SceneManager.LoadScene("Stage");
    }

#region Setting Functions

    public void FinishSetting()
    {
        //Save Setting Value
        Time.timeScale = 1;
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
