using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyRangeStats : MonoBehaviour
{
    Animator anim;
    EnemyRange enemyRange;
    Weapons weapon;
    EnemyGun gun;

    public EnemyBaseStatus enemyDataStat;
    public Slider healthBar;
    public float currentHealth;
    
    void Start()
    {
        enemyRange = GetComponent<EnemyRange>();
        gun = GetComponentInChildren<EnemyGun>();
        anim = GetComponentInChildren<Animator>();

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
                enemyRange.Knockback();
                currentHealth -= weapon.damage;
                gun.isFiring = false;
                anim.SetTrigger("Hurt");
            }
            else
            {

            }
        }

        if(other.tag == "Barrier")
        {
            enemyRange.Knockback();
        }     
    }
}
