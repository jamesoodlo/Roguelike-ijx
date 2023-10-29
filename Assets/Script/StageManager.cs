using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    PlayerStats playerStats;
    public bool endPhase = false;
    public StageData stageData;
    public int enemiesCount;
    private GameObject[] enemies;
    public GameObject portal;

    void Start()
    {
        portal.SetActive(false);

        playerStats = FindObjectOfType<PlayerStats>();

        if(stageData.HighestStage < stageData.currentStage) stageData.HighestStage = stageData.currentStage;
    }

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesCount = enemies.Length;

        if(playerStats.currentHealth <= 0) endPhase = true;

        if(endPhase)
        {
            if(stageData.HighestStage < stageData.currentStage) stageData.HighestStage = stageData.currentStage;
        }
        
        if(enemiesCount <= 0)
        {
            portal.SetActive(true);
        }
    }
}
