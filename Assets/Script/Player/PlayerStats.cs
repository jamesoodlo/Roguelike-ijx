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
    Inventory inven;
    SoundFx soundFx;

    public BaseStatus playerDataStat;
    public UIManager uiManager;
    public GameObject hurtPanel;

    public bool isHurt;
    public float timeSinceLastHurt = 0;

    public float currentHealth;
    public float currentStamina;
    public float regenStaminaRate; 
    private float timeSinceLastRegen = 0f;
    
    public float currentGuard;
    public float regenGuardRate = 5;
    private float timeSinceLastRegenGuard = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        inven = GetComponent<Inventory>();
        soundFx = GetComponent<SoundFx>();
        animHandle = GetComponent<AnimationHandle>();
        inputHandle = GetComponent<InputHandle>();
        playerControll = GetComponent<PlayerController>();

        currentHealth = playerDataStat.maxHealth;
        currentStamina = playerDataStat.maxStamina;
        regenStaminaRate = playerDataStat.maxStaminaRegen;
        currentGuard = playerDataStat.maxGuard;
    }

    void Update()
    {
        RegenrateStamina();
        RegenrateGuard();
        ExpLevel();

        hurtPanel.SetActive(isHurt);

        if(isHurt)
        {
            timeSinceLastHurt += Time.deltaTime;
            inputHandle.attack = false;

            if(timeSinceLastHurt > 0.4f)
            {
                isHurt = false;
                timeSinceLastHurt = 0;
            }
        }
        else
        {
            timeSinceLastHurt = 0;
        }

        if(currentHealth <= 0) 
        {
            Time.timeScale = 0;
    
            currentHealth = 0;
            uiManager.lastStageText.SetActive(false);
            uiManager.gameOverPanel.SetActive(true);
        }

        if(currentStamina < 0) currentStamina = 0;
        if(currentGuard < 0) currentGuard = 0;
        if(currentHealth > playerDataStat.maxHealth) currentHealth = playerDataStat.maxHealth; 
        if(currentStamina > playerDataStat.maxStamina) currentStamina = playerDataStat.maxStamina;
        if(currentGuard > playerDataStat.maxGuard) currentGuard = playerDataStat.maxGuard;
    }

    public void RegenrateStamina()
    { 
        timeSinceLastRegen += Time.deltaTime;

        if (currentStamina < playerDataStat.maxStamina)
        { 
            if (timeSinceLastRegen >= 1.0f)
            {
                currentStamina += regenStaminaRate * Time.deltaTime;
            }
        }
    }

    public void RegenrateGuard()
    { 
        if (currentGuard < playerDataStat.maxGuard)
        {
            timeSinceLastRegenGuard += Time.deltaTime;

            if (timeSinceLastRegenGuard >= 1.5f)
            {
                currentGuard += regenGuardRate * Time.deltaTime;
            }
        }
    }

    public void ExpLevel()
    {
        if(playerDataStat.exp >= playerDataStat.maxExp)
        {
            playerDataStat.level += 1;
            playerDataStat.maxExp *= 2;
            playerDataStat.canLvUp = true;
            playerDataStat.exp = 0;
        }
    }

    IEnumerator ResetKnockBack(float delay = 0.1f)
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EnemyDmg")
        {
            if(other.GetComponent<EnemyWeapon>().onAttack && !inven.isBarrier && !playerControll.isSuperDuck && !playerControll.isGuard && !playerControll.isParried && !playerControll.isRolling)
            {
                currentHealth -= other.GetComponent<EnemyWeapon>().damage;
                anim.SetTrigger("Hurt");
                soundFx.hurtSfx.Play();
            }
            else if(other.GetComponent<EnemyWeapon>().onAttack && playerControll.isGuard && !playerControll.isParried && !playerControll.isRolling)
            {  
                currentHealth -= other.GetComponent<EnemyWeapon>().damage * 65 /100;
                currentGuard -= other.GetComponent<EnemyWeapon>().damage;

                float knockbackSpeed = 4.5f;
                rb.AddForce(-transform.forward * knockbackSpeed, ForceMode.Impulse);

                soundFx.blockingSfx.Play();

                anim.Play("Blocked");
                    
                StartCoroutine(ResetKnockBack());
            }
            else if(other.GetComponent<EnemyWeapon>().onAttack && playerControll.isSuperDuck)
            {
                soundFx.hurtSfx.Play();
                currentHealth -= other.GetComponent<EnemyWeapon>().damage * 55 / 100;
            }
            else if(other.GetComponent<EnemyWeapon>().onAttack && inven.isBarrier)
            {
                currentHealth -= other.GetComponent<EnemyWeapon>().damage * 65 / 100;
            }
            else if(other.GetComponent<EnemyWeapon>().onAttack && other.GetComponent<EnemyWeapon>().isParried)
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

        if(other.gameObject.tag == "Exp")
        {
            playerDataStat.exp += 0.5f;
        }
    }
}
