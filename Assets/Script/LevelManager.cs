using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public EnemyMelee[] enemyMelee;

    void Start()
    {
        
    }

    void Update()
    {
        enemyMelee = FindObjectsOfType<EnemyMelee>();

        for (int i = 0; i < enemyMelee.Length; i++)
        {
            
        }
    }
}
