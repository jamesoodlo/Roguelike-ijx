using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eggs : MonoBehaviour
{
    Items item;

    public BaseStatus playerDataStat;

    private int[] eggsValue = {1, 3, 5};
    public int getEggsValue;
    public GameObject[] eggsObj;

    void Start()
    {
        item = GetComponent<Items>();
        getEggsValue = eggsValue[Random.Range(0, eggsValue.Length)];

        eggsObj[0].SetActive(false);
        eggsObj[1].SetActive(false);
        eggsObj[2].SetActive(false);
    }

    void Update()
    {
        if(getEggsValue == 1)
        {
            eggsObj[0].SetActive(true);
        }
        else if(getEggsValue == 3)
        {
            eggsObj[1].SetActive(true);
        }
        else if(getEggsValue == 5)
        {
            eggsObj[2].SetActive(true);
        }

        Interacted();
    }

    public void Interacted()
    {        
        if(item.isInteracted)
        {
            playerDataStat.currentEggs += getEggsValue;

            item.isInteracted = false;

            Destroy(this.gameObject);
        }
    }
}
