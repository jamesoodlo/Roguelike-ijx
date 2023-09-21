using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOrb : MonoBehaviour
{
    private float timeSinceHeal;
    private bool playerTrigger = false;

    void Start()
    {

    }

    void Update()
    {
        if(playerTrigger)
        {
            timeSinceHeal += Time.deltaTime;
        }

        if(timeSinceHeal >= 1.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            playerTrigger = true;   
        }    
    }
}
