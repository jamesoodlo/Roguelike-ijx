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

    [Header("Exp & Point")]
    private int[] dropRate = {0, 1, 2}; 
    private int getDropRate;
    public GameObject Point;
    public GameObject Exp;
    
    
    void Start()
    {
        enemyMelee = GetComponent<EnemyMelee>();
        anim = GetComponentInChildren<Animator>();
        healthBar = GetComponentInChildren<Slider>();

        currentHealth = enemyDataStat.maxHealth;
        getDropRate = dropRate[Random.Range(0, dropRate.Length)];
    }

    void Update()
    {
        SetHealthBar();

        weapon = FindObjectOfType<Weapons>();

        if(currentHealth <= 0)
        {
            Instantiate(Exp, transform.position, transform.rotation);

            if(getDropRate == 0) Instantiate(Point, transform.position, transform.rotation);
            
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
        if(other.tag == "Weapons" || other.tag == "SlashProjectile")
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
