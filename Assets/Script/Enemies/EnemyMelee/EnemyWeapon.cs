using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    Animator anim;
    PlayerController playerController;
    EnemyMelee enemyMelee;

    public EnemyBaseStatus enemyDataStat;
    public bool onAttack = false;
    public float damage;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        enemyMelee = GetComponentInParent<EnemyMelee>();

        damage = enemyDataStat.attackDamage;
    }

    void Update()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Shield")
        {
            if(playerController.isParried)
            {
                anim.SetTrigger("Parried");
                enemyMelee.Knockback();
                enemyMelee.isParried = true;
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
