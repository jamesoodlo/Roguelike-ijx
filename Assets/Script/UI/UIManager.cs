using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public BaseStatus playerDataStat;
    public Slider healthBar;

    void Start()
    {
        healthBar.maxValue = playerDataStat.maxHealth;
    }

    void Update()
    {
        SetHealthBar();
    }

    public void SetHealthBar()
    {
        PlayerStats stats = FindObjectOfType<PlayerStats>();
        healthBar.value = stats.currentHealth;

    }
}
