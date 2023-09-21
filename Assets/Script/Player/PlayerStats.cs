using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    InputHandle inputHandle;
    AnimationHandle animHandle;
    PlayerController playerControll;

    EnemyWeapon enemyWeapon;

    public BaseStatus playerDataStat;

    public float currentHealth;
    public int currentStamina;
    public int regenStaminaRate = 1; 
    private float timeSinceLastRegen = 0f;
    public GameObject[] staminaPoint;
    
    private bool isHurt = false;

    public float currentBlocked;
    public float regenBlockedRate = 1;
    private float timeSinceLastRegenBlocked = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        inputHandle = GetComponent<InputHandle>();
        animHandle = GetComponent<AnimationHandle>();
        playerControll = GetComponent<PlayerController>();

        currentHealth = playerDataStat.maxHealth;
        currentStamina = playerDataStat.maxStamina;
        currentBlocked = playerDataStat.maxBlocked;
    }

    void Update()
    {
        DrainStaminaPoint();
        RegenrateStamina();
        RegenrateBlocked();

        if(isHurt)
        {
            inputHandle.move = Vector2.zero;
            inputHandle.attack = false;
        }
        else
        {

        }

        if(currentHealth < 0) currentHealth = 0;
        if(currentHealth > playerDataStat.maxHealth) currentHealth = playerDataStat.maxHealth; 

        enemyWeapon = FindObjectOfType<EnemyWeapon>();
    }

    public void RegenrateStamina()
    { 
        if (currentStamina < playerDataStat.maxStamina)
        {
            timeSinceLastRegen += Time.deltaTime;

            if (timeSinceLastRegen >= 1.5f && !playerControll.isAttacking)
            {
                int regenAmount = Mathf.Min(playerDataStat.maxStamina - currentStamina, regenStaminaRate);
                currentStamina += regenAmount;
                timeSinceLastRegen = 0f;
            }
        }
    }

    public void RegenrateBlocked()
    { 
        if (currentBlocked < playerDataStat.maxBlocked)
        {
            timeSinceLastRegenBlocked += Time.deltaTime;

            if (timeSinceLastRegenBlocked >= 2.0f)
            {
                float regenAmount = Mathf.Min(playerDataStat.maxBlocked - currentBlocked, regenBlockedRate);
                currentBlocked += regenAmount;
                timeSinceLastRegenBlocked = 0f;
            }
        }
    }
    
    private void DrainStaminaPoint()
    {
        if(currentStamina == 5)
        {
            staminaPoint[0].SetActive(true);
            staminaPoint[1].SetActive(true);
            staminaPoint[2].SetActive(true);
            staminaPoint[3].SetActive(true);
            staminaPoint[4].SetActive(true);
        }
        else if(currentStamina == 4)
        {
            staminaPoint[0].SetActive(false);
            staminaPoint[1].SetActive(true);
            staminaPoint[2].SetActive(true);
            staminaPoint[3].SetActive(true);
             staminaPoint[4].SetActive(true);
        }
        else if(currentStamina == 3)
        {
            staminaPoint[0].SetActive(false);
            staminaPoint[1].SetActive(false);
            staminaPoint[2].SetActive(true);
            staminaPoint[3].SetActive(true);
             staminaPoint[4].SetActive(true);
        }
        else if(currentStamina == 2)
        {
            staminaPoint[0].SetActive(false);
            staminaPoint[1].SetActive(false);
            staminaPoint[2].SetActive(false);
            staminaPoint[3].SetActive(true);
             staminaPoint[4].SetActive(true);
        }
        else if(currentStamina == 1)
        {
            staminaPoint[0].SetActive(false);
            staminaPoint[1].SetActive(false);
            staminaPoint[2].SetActive(false);
            staminaPoint[3].SetActive(false);
             staminaPoint[4].SetActive(true);
        }
        else
        {
            staminaPoint[0].SetActive(false);
            staminaPoint[1].SetActive(false);
            staminaPoint[2].SetActive(false);
            staminaPoint[3].SetActive(false);
            staminaPoint[4].SetActive(false);
        }
    }

    public void GetHurt()
    {
        anim.SetTrigger("Hurt");

        float knockbackSpeed = 5.0f;
        rb.AddForce(-transform.forward * knockbackSpeed, ForceMode.Impulse);

        StartCoroutine(ResetKnockBack());
        StartCoroutine(HurtDontMove());
    }

    IEnumerator ResetKnockBack(float delay = 0.1f)
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector3.zero;
    }

    IEnumerator HurtDontMove(float delay = 0.5f)
    {
        isHurt = true;
        yield return new WaitForSeconds(delay);
        isHurt = false;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "EnemyDmg")
        {
            if(playerControll.isBlocking && enemyWeapon.onAttack && !playerControll.isParried)
            {  
                currentHealth -= enemyWeapon.damage * 50 /100;
                currentBlocked -= enemyWeapon.damage;

                float knockbackSpeed = 4.5f;
                rb.AddForce(-transform.forward * knockbackSpeed, ForceMode.Impulse);

                anim.Play("Blocked");
                
                StartCoroutine(ResetKnockBack());
            }
            else if(enemyWeapon.onAttack)
            {
                currentHealth -= enemyWeapon.damage;
                GetHurt();
            }
            else if(playerControll.isBarrier && enemyWeapon.onAttack)
            {
                currentHealth -= enemyWeapon.damage * 80 / 100;
            }
            else if(playerControll.isRolling && enemyWeapon.onAttack || playerControll.isParried && enemyWeapon.onAttack)
            {
                //no dmg
            }
            else
            {
                //no dmg
            }
            
        }
        else
        {
            //no dmg
        }

        if(other.gameObject.tag == "BulletEnemy")
        {
            if(playerControll.isBarrier)
            {
                //no dmg
            }
            else if(playerControll.isBlocking)
            {
                currentBlocked -= other.GetComponent<bullet>().damage;
                anim.Play("Blocked");
            }
            else
            {
                currentHealth -= other.GetComponent<bullet>().damage;
            }
        } 
        else
        {
            //no dmg
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.tag == "Heal")
        {
            currentHealth += 3.5f * Time.deltaTime;
        }   
    }

}
