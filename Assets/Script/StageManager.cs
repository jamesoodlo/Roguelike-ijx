using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public StageData stageData;
    public int enemiesCount;
    private GameObject[] enemies;
    public GameObject portal;

    void Start()
    {
        portal.SetActive(false);
    }

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesCount = enemies.Length;
        
        if(enemiesCount <= 0)
        {
            portal.SetActive(true);
        }
    }
}
