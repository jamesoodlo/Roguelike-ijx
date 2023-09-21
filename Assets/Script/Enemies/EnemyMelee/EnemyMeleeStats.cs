using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyMeleeStats : MonoBehaviour
{
    Animator anim;
    EnemyMelee enemyMelee;
    Slider healthBar;
    Weapons weapon;

    public EnemyBaseStatus enemyDataStat;
    public float currentHealth;
    
    void Start()
    {
        enemyMelee = GetComponent<EnemyMelee>();
        anim = GetComponentInChildren<Animator>();
        healthBar = GetComponentInChildren<Slider>();

        currentHealth = enemyDataStat.maxHealth;
    }

    void Update()
    {
        SetHealthBar();

        weapon = FindObjectOfType<Weapons>();

        if(currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetHealthBar()
    {
        healthBar.maxValue = enemyDataStat.maxHealth;
        healthBar.value = currentHealth;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Weapons")
        {
            if(weapon.onAttack)
            {
                enemyMelee.Knockback();
                currentHealth -= weapon.damage;
                anim.Play("Hurt");
            }
            else
            {

            }
        }

        if(other.tag == "Barrier")
        {
            enemyMelee.Knockback();
        }     
    }
}
