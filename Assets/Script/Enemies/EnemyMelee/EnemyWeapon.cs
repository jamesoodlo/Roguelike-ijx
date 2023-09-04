using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    Animator anim;
    private PlayerController playerController;
    private EnemyMelee enemyMelee;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        enemyMelee = GetComponentInParent<EnemyMelee>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            playerController = other.transform.GetComponent<PlayerController>();
            if(playerController.isParried)
            {
                anim.SetTrigger("Hurt");
            }
            else
            {

            }
        }    
        else
        {
            
        }
    }
}
