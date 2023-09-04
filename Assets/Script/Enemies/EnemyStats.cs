using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    Animator anim;

    public BaseStatus playerDataStat;
    public EnemyBaseStatus enemyDataStat;

    public float currentHealth;
    
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        currentHealth = enemyDataStat.maxHealth;
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Weapons")
        {
            currentHealth -= playerDataStat.attackDamage;
            anim.SetTrigger("Hurt");
        }    
    }
}
