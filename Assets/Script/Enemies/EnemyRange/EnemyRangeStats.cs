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
    public float currentHealth;
    public Slider healthBar;

    [Header("Exp & Point")]
    private int[] dropRate = {0, 1, 2}; 
    private int getDropRate;
    public GameObject Point;
    public GameObject Exp;
    
    
    void Start()
    {
        enemyRange = GetComponent<EnemyRange>();
        gun = GetComponentInChildren<EnemyGun>();
        anim = GetComponentInChildren<Animator>();

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
        if(other.tag == "Weapons" || other.tag == "SlashProjectile" )
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
