using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
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
            if(playerDataStat.item2Num < playerDataStat.item2Pocket)
            {
                playerDataStat.item2Num += 1;

                item.isInteracted = false;

                Destroy(this.gameObject);
            }
        }
    }
}
