using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private GameObject findPlayer;
    public StageData stageData;
    public GameObject interactText;
    public GameObject nextStagePanel;
    public Interaction player;
    public float distanceFromTarget;

    void Start()
    {
        nextStagePanel.SetActive(false);
        interactText.SetActive(false);

        findPlayer = GameObject.Find("Player");
        player = findPlayer.GetComponent<Interaction>();
    }

    
    void Update()
    {
        distanceFromTarget = Vector3.Distance(player.transform.position, transform.position);
        Interacted();
    }

    public void Interacted()
    {
        if(distanceFromTarget <= 2) 
        {
            interactText.SetActive(true);
        }
        else
        {
            interactText.SetActive(false);
        }

        if(distanceFromTarget <= 2 && player.isInteract)
        {
            if(stageData.currentStage  % 5 == 0)
            {
                nextStagePanel.SetActive(true);

                Time.timeScale = 0;
            }
            else
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
            }
        }
    }
}
