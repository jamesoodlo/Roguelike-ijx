using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    Items item;

    public BaseStatus playerDataStat;

    void Start()
    {
        item = GetComponent<Items>();
    }

    void Update()
    {
        Interacted();
    }

    public void Interacted()
    {        
        if(item.isInteracted)
        {
            if(playerDataStat.item1Num < playerDataStat.item1Pocket)
            {
                playerDataStat.item1Num += 1;

                item.isInteracted = false;

                Destroy(this.gameObject);
            }
        }
    }
}
