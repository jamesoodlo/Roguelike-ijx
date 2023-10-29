using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputHandle inputHandle;
    AnimationHandle animHandle;
    PlayerStats stats;
    Rigidbody rb;
    SoundFx soundFx;

    public BaseStatus playerDataStat;

    [Header("Movement")]
    public float speed;
    public float rotationSpeed = 10f;

    [Header("Roll")]
    public float roll;
    public float rollCooldown;
    public float rollingTime;
    public bool isRolling = false;
    private bool canRoll = true;

    [Header("Attack")]
    public bool isAttacking;
    public int currentAttack = 0;
    private float timeSinceAttack;

    [Header("Guard & Parry")]
    public GameObject guardBar;
    public GameObject guardObj;
    public bool isGuard;
    public bool isParried;
    private float timeSinceGuard;

    [Header("Skill DuckSlash")]
    public SlashSkill slashSkill;
    public bool onSlash;
    public bool isSlash;
    public float slashTime;
    public float maxSlashTime;
    public float maxSlashCdTime;
    public float slashCdTime;
    public bool canSlash = true;
    private bool isSlashSfx = true;

    [Header("Skill SuperDuck")]
    public bool isSuperDuck;
    public float superDuckTime;
    public float maxSuperDuckTime;
    public float maxSuperDuckCdTime;
    public float superDuckCdTime;
    public bool canSuperDuck = true;
    private bool setStatBuff = false;

    void Start()
    {
        inputHandle = GetComponent<InputHandle>();
        animHandle = GetComponent<AnimationHandle>();
        soundFx = GetComponent<SoundFx>();
        stats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody>();

        guardObj.SetActive(false);
        superDuckCdTime = maxSuperDuckCdTime;

        speed = playerDataStat.speed;
        slashTime = maxSlashTime;
        superDuckTime = maxSuperDuckTime;

        playerDataStat.currentMaxHealth = playerDataStat.maxHealth;
        playerDataStat.currentAttackDamage = playerDataStat.attackDamage; 
        playerDataStat.currentSpeed = playerDataStat.speed;
    }

    void Update()
    {    
        timeSinceAttack += Time.deltaTime;

        slashSkill.onSlash = onSlash;

        Attack();
        Guard();
        SkillQ();
        SkillE(); 

        if(isGuard || isAttacking || stats.isHurt)
        {
            //if Guard cant move
        }
        else
        {
            movePlayer(); 
        }

        if(stats.currentStamina > 15f) Rolling(); 
        if(stats.currentHealth <= 0) Destroy(this);
        
        
        guardBar.SetActive(isGuard);
    }

#region Movement System
    public void movePlayer()
    {
        Vector3 movement = new Vector3(inputHandle.move.x, 0f, inputHandle.move.y);

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    private void Rolling()
    {
        if(inputHandle.dash && inputHandle.move != Vector2.zero && canRoll && !isAttacking)
        {

            if(canRoll) 
            {
                Vector3 rollDir = new Vector3(inputHandle.move.x, 0f, inputHandle.move.y);

                rollDir.y = 0;

                rb.AddForce(rollDir * roll, ForceMode.VelocityChange);

                canRoll = false;
                isRolling = true;
                animHandle.RollAnimation();
                if(!canRoll)
                {
                    StartCoroutine(rollCd());
                    StartCoroutine(rollingTimer());
                }
            }
            else
            {
                
            }
        }
        else
        {

        }
    }
#endregion

#region Combat System
    private void Attack()
    {
        if(stats.currentStamina > 15f && !isRolling)
        {
            if (inputHandle.attack && timeSinceAttack > 0.5f)
            {
                currentAttack++;
                isAttacking = true;
                AttackForward();
                
                inputHandle.move = Vector2.zero;
                
                if (currentAttack > 3)
                    currentAttack = 1;

                //Reset Attack When Time out
                if (timeSinceAttack > 1.0f)
                    currentAttack = 1;

                //Call Trigger Attack Animation
                animHandle.AttackAnimation();
                
                //Reset Timer
                timeSinceAttack = 0;
            }
        }
        else
        {

        }
    }

    public void AttackForward()
    {        
        float forwardSpeed = 15f;

        rb.AddForce(transform.forward * forwardSpeed, ForceMode.Impulse);

        //StartCoroutine(setZeroAtkFw());
    }

    private void Guard()
    {
        if(inputHandle.guard && stats.currentGuard > 0)
        {
            timeSinceGuard += Time.deltaTime;
            isGuard = true;
            guardObj.SetActive(true);
                
            if(timeSinceGuard < 0.25f)
            {
                isParried = true;
            }
            else
            {
                isParried = false;
            }

            if(timeSinceGuard > 3.0f)
            {
                isGuard = false;
                inputHandle.guard = false;
                guardObj.SetActive(false);
            }

            if(stats.currentGuard <= 0)
            {
                timeSinceGuard = 0;
                isGuard = false;
                inputHandle.guard = false;
                guardObj.SetActive(false);
            }
        }
        else
        {
            timeSinceGuard = 0;
            isParried = false;
            isGuard = false;
            guardObj.SetActive(false);
        }
    }
#endregion
    
#region Skill System
    private void SkillQ()
    {   
        if(playerDataStat.hasSlasher)
        {
            if(!canSlash)  slashCdTime -= Time.deltaTime;
            if(isSlash) 
            {
                slashTime -= Time.deltaTime;

                if(isSlashSfx) 
                    soundFx.slashbuffSfx.Play();
                    isSlashSfx = false;
                
            }

            if(slashTime <= 0) 
            {
                slashTime = 0;
                isSlash = false;
                isSlashSfx = true;
            }

            if(slashCdTime <= 0) 
            {
                canSlash = true;
                slashCdTime = maxSlashCdTime;
            }

            if(canSlash) slashTime = maxSlashTime;

            if(inputHandle.skillQ && canSlash && slashCdTime == maxSlashCdTime)
            {
                if(canSlash)
                {
                    isSlash = true;
                    
                    canSlash = false;
                }
            }
            else
            {
                
            }
        }
        else
        {

        }
    }

    private void SkillE()
    {
        //Set Lv Skill
        if(playerDataStat.superDuckLv == 1)
        {
            maxSuperDuckTime = 3.0f;
        }
        else if(playerDataStat.superDuckLv == 2)
        {
            maxSuperDuckTime = 4.0f;
        }
        else if(playerDataStat.superDuckLv == 3)
        {
            maxSuperDuckTime = 5.0f;
        }
        else if(playerDataStat.superDuckLv == 4)
        {
            maxSuperDuckTime = 6.0f;
        }
        else if(playerDataStat.superDuckLv == 5)
        {
            maxSuperDuckTime = 7.0f;
        }
        else if(playerDataStat.superDuckLv == 6)
        {
            maxSuperDuckTime = 8.0f;
        }
        else if(playerDataStat.superDuckLv == 7)
        {
            maxSuperDuckTime = 9.0f;
        }
        else if(playerDataStat.superDuckLv == 8)
        {
            maxSuperDuckTime = 10.0f;
        }
        else if(playerDataStat.superDuckLv == 9)
        {
            maxSuperDuckTime = 11.0f;
        }
        else if(playerDataStat.superDuckLv == 10)
        {
            maxSuperDuckTime = 12.0f;
        }
        
        //Set Status Buff
        if(setStatBuff)
        {
            setStatBuff = false;
            stats.currentHealth += playerDataStat.maxHealthBuff; 
            playerDataStat.maxHealth += playerDataStat.maxHealthBuff;
            playerDataStat.attackDamage += playerDataStat.attackDamageBuff;
            playerDataStat.speed += playerDataStat.speedBuff;  
        }

        //Skill Mechanic
        if(playerDataStat.hasSuperDuck)
        {
            if(!canSuperDuck) superDuckCdTime -= Time.deltaTime;

            if(isSuperDuck) 
            {
                superDuckTime -= Time.deltaTime;
            }

            if(superDuckTime <= 0) 
            {
                superDuckTime = 0;

                playerDataStat.maxHealth = playerDataStat.currentMaxHealth;
                playerDataStat.attackDamage = playerDataStat.currentAttackDamage;
                playerDataStat.speed = playerDataStat.currentSpeed;  

                isSuperDuck = false;
            }

            if(superDuckCdTime <= 0) 
            {
                canSuperDuck = true;
                superDuckCdTime = maxSuperDuckCdTime;
            }

            if(canSuperDuck) superDuckTime = maxSuperDuckTime;

            if(inputHandle.skillE && canSuperDuck && !isAttacking && superDuckCdTime == maxSuperDuckCdTime && !isSuperDuck)
            {
                if(canSuperDuck)
                {
                    isSuperDuck = true;

                    setStatBuff = true;
                    
                    canSuperDuck = false;

                    animHandle.SuperDuckAnimation();
                }
            }
            else
            {
                
            }
        }
        else
        {
            
        }
    }  
#endregion

    private IEnumerator rollingTimer()
    {
        isRolling = true;
        yield return new WaitForSeconds(rollingTime);
        isRolling = false;
        rb.velocity = Vector3.zero;
    }

    private IEnumerator rollCd()
    {
        canRoll = false;
        yield return new WaitForSeconds(rollCooldown);
        canRoll = true;
    }

    private IEnumerator setZeroAtkFw(float delay = 1.5f)
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector3.zero;
    }

}
