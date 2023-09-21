using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public BaseStatus playerDataStat;
    public Slider healthBar;
    public Slider shieldBar;
    public Slider[] cdBar;

    void Start()
    {
        healthBar.maxValue = playerDataStat.maxHealth;
        shieldBar.maxValue = playerDataStat.maxBlocked;
        
    }

    void Update()
    {
        SetHealthBar();
        SetShieldBar();
        SetSkill();
    }

    public void SetHealthBar()
    {
        PlayerStats stats = FindObjectOfType<PlayerStats>();
        shieldBar.value = stats.currentBlocked;
    }

    public void SetShieldBar()
    {
        PlayerStats stats = FindObjectOfType<PlayerStats>();
        healthBar.value = stats.currentHealth;
    }

    public void SetSkill()
    {
        PlayerController playerControll = FindObjectOfType<PlayerController>();
        cdBar[1].maxValue = playerControll.maxBarrierCdTime;
        cdBar[1].value = playerControll.barrierCdTime;

    }
}
