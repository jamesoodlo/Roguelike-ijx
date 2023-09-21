using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    Weapons weapon;

    private bool spawned;
    private int itemsCount;
    public Transform spawnPoint;
    private GameObject ItemsAfterRandom;
    public GameObject[] Items;
    

    void Start()
    {
        ItemsAfterRandom = Items[Random.Range(0, Items.Length)];
    }

    void Update()
    {
        weapon = FindObjectOfType<Weapons>();

        if(spawned)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Weapons")
        {
            if(weapon.onAttack)
            {   
                while(itemsCount < 1)
                {
                    Instantiate(ItemsAfterRandom, spawnPoint.position, Quaternion.identity);
                    itemsCount += 1;  
                }  
                
                spawned = true;
            }
            else
            {

            }
        }    
    }
}
