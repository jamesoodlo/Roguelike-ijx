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

    [Header("Block & Parry")]
    public GameObject shieldObj;
    public bool isBlocking;
    public bool isParried;
    private float timeSinceBlock;

    [Header("Skill Slash")]
    public bool hasSlasher;
    public SlashSkill slashSkill;
    public bool onSlash;
    public bool isSlash;
    public float slashTime;
    public float maxSlashTime;
    public float maxSlashCdTime;
    public float slashCdTime;
    public bool canSlash = true;

    [Header("Skill Barrier")]
    public bool hasBarrier;
    public GameObject barrierObj;
    public bool isBarrier;
    public float barrierTime;
    public float maxBarrierTime;
    public float maxBarrierCdTime;
    public float barrierCdTime;
    public bool canBarrier = true;

    void Start()
    {
        inputHandle = GetComponent<InputHandle>();
        animHandle = GetComponent<AnimationHandle>();
        stats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody>();

        barrierObj.SetActive(false);
        shieldObj.SetActive(false);
        barrierCdTime = maxBarrierCdTime;

        speed = playerDataStat.speed;
        slashTime = maxSlashTime;
        barrierTime = maxBarrierTime;
    }

    void Update()
    {    
        timeSinceAttack += Time.deltaTime;

        slashSkill.onSlash = onSlash;

        hasBarrier = playerDataStat.hasBarrier;
        hasSlasher = playerDataStat.hasSlasher;

        Attack();
        Block();
        SkillQ();
        SkillE(); 

        if(isBlocking || isAttacking)
        {
            //if blocking cant move
        }
        else
        {
            movePlayer();    
        }

        if(stats.currentStamina > 0) Rolling(); 
        

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
        if(stats.currentStamina > 0 && !isRolling)
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

    private void Block()
    {
        if(inputHandle.block && stats.currentShield >= 1)
        {
            timeSinceBlock += Time.deltaTime;
            isBlocking = true;
            shieldObj.SetActive(true);
                
            if(timeSinceBlock < 0.25f)
            {
                isParried = true;
            }
            else
            {
                isParried = false;
            }

            if(timeSinceBlock > 3.0f)
            {
                isBlocking = false;
                inputHandle.block = false;
                shieldObj.SetActive(false);
            }

            if(stats.currentShield <= 0)
            {
                timeSinceBlock = 0;
                isBlocking = false;
                inputHandle.block = false;
                shieldObj.SetActive(false);
            }
        }
        else
        {
            timeSinceBlock = 0;
            isParried = false;
            isBlocking = false;
            shieldObj.SetActive(false);
        }
    }
#endregion
    
#region Skill System
    private void SkillQ()
    {
        if(hasSlasher)
        {
            if(!canSlash)  slashCdTime -= Time.deltaTime;
            if(isSlash) slashTime -= Time.deltaTime;

            if(slashTime <= 0) 
            {
                slashTime = 0;
                isSlash = false;
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
        if(hasBarrier)
        {
            if(!canBarrier)  barrierCdTime -= Time.deltaTime;
            if(isBarrier) barrierTime -= Time.deltaTime;

            if(barrierTime <= 0) 
            {
                barrierTime = 0;
                barrierObj.SetActive(false);
                isBarrier = false;
            }

            if(barrierCdTime <= 0) 
            {
                canBarrier = true;
                barrierCdTime = maxBarrierCdTime;
            }

            if(canBarrier) barrierTime = maxBarrierTime;

            if(inputHandle.skillE && canBarrier && !isAttacking && barrierCdTime == maxBarrierCdTime)
            {
                if(canBarrier)
                {
                    isBarrier = true;
                    
                    barrierObj.SetActive(true);

                    canBarrier = false;

                    animHandle.BarrierAnimation();
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
