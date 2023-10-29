using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    InputHandle inputHandle;
    PlayerStats playerStats;
    SoundFx soundFx;

    public BaseStatus playerDataStat;

    public GameObject barrierObj;
    public bool canBarrier = true;
    public bool isBarrier = false;
    public float barrierTime;
    public float maxBarrierTime;

    void Start()
    {
        inputHandle = GetComponent<InputHandle>();
        playerStats = GetComponent<PlayerStats>();
        soundFx = GetComponent<SoundFx>();

        barrierTime = maxBarrierTime;
    }

    void Update()
    {
        UseItems();
        ActivateMushroom();
        barrierObj.SetActive(isBarrier);
    }

    public void UseItems()
    {
        if(playerDataStat.item1Num > 0 && inputHandle.item1)
        {
            playerDataStat.item1Num -= 1;
            soundFx.EatApple.Play();
            inputHandle.item1 = false;
            playerStats.currentHealth += 30;
        }

        if(playerDataStat.item2Num > 0 && inputHandle.item2 && !isBarrier && canBarrier)
        {
            playerDataStat.item2Num -= 1;
            soundFx.EatApple.Play();
            isBarrier = true;
            inputHandle.item1 = false;
        }
    }

    public void ActivateMushroom()
    {
        if(isBarrier) 
        {
            canBarrier = false;
            barrierTime -= Time.deltaTime;
        }

        if(barrierTime <= 0) 
        {
            barrierTime = 0;
            isBarrier = false;
            canBarrier = true;
        }

        if(canBarrier) barrierTime = maxBarrierTime;
    }
}
