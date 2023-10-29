using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    Items item;
    private PlayerController playerControll;
    public StageData stageData;
    public BaseStatus playerDataStat;
    public UIManager uiManager;
    public StageManager stageManager;
    public GameObject nextStagePanel;

    void Start()
    {
        item = GetComponent<Items>();

        playerControll = FindObjectOfType<PlayerController>();

        nextStagePanel.SetActive(false);
    }

    
    void Update()
    {
        Interacted();
    }

    public void Interacted()
    {        
        if(item.isInteracted)
        {
            if(stageData.currentStage  % 5 == 0 && stageData.currentStage != stageData.maxStage)
            {
                nextStagePanel.SetActive(true);

                Time.timeScale = 0;
            }
            else
            {
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
                    stageManager.endPhase = true;
                    
                    uiManager.gameOverPanel.SetActive(true);
                    uiManager.lastStageText.SetActive(true);
                    uiManager.gameOverText.SetActive(false);

                    playerControll.isSlash = false;
                    playerDataStat.maxHealth = playerDataStat.currentMaxHealth;
                    playerDataStat.attackDamage = playerDataStat.currentAttackDamage;
                    playerDataStat.speed = playerDataStat.currentSpeed;
                    
                    Time.timeScale = 0;
                }   
            }
        }
    }
}
