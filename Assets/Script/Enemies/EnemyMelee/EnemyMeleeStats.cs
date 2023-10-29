using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyMeleeStats : MonoBehaviour
{
    Animator anim;
    EnemyMelee enemyMelee;
    Slider healthBar;
    SoundFx soundFx;

    public EnemyBaseStatus enemyDataStat;
    public Collider collider;
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
        soundFx = GetComponent<SoundFx>();
        healthBar = GetComponentInChildren<Slider>();

        currentHealth = enemyDataStat.maxHealth;
        getDropRate = dropRate[Random.Range(0, dropRate.Length)];
    }

    void Update()
    {
        SetHealthBar();

        if(currentHealth <= 0)
        {
            anim.Play("Death");

            collider.enabled = false;

            StartCoroutine(DeathDelay());
        }
    }

    public void SetHealthBar()
    {
        healthBar.maxValue = enemyDataStat.maxHealth;
        healthBar.value = currentHealth;
    }

    IEnumerator DeathDelay(float delay = 1.99f)
    {
        yield return new WaitForSeconds(delay);

        Instantiate(Exp, transform.position, transform.rotation);

        if(getDropRate == 0) Instantiate(Point, transform.position, transform.rotation);

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Weapons")
        {
            if(other.GetComponent<Weapons>().onAttack)
            {
                enemyMelee.Knockback();
                currentHealth -= other.GetComponent<Weapons>().damage;
                anim.Play("Hurt");
                soundFx.hurtSfx.Play();
            }
            else
            {

            }
        }

        if(other.tag == "SlashProjectile")
        {
            currentHealth -= other.GetComponent<SlashProjectile>().damage;
            enemyMelee.Knockback();
            anim.Play("Hurt");
            soundFx.hurtSfx.Play();
        }

        if(other.tag == "Barrier")
        {
            enemyMelee.Knockback();
        }     
    }
}
