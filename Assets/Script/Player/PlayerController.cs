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

    public Collider collider;

    [Header("Movement")]
    public float speed;
    public float rotationSpeed = 10f;

    [Header("Roll")]
    public float roll;
    public float rollCooldown;
    public float rollingTime;
    public bool isRolling = false;
    public bool canRoll = true;

    [Header("Combat")]
    public bool isAttacking;
    public int currentAttack = 0;
    private float timeSinceAttack;

    public bool isBlocking;
    public bool isParried;
    private float timeSinceBlock;

    void Start()
    {
        inputHandle = GetComponent<InputHandle>();
        animHandle = GetComponent<AnimationHandle>();
        stats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {    
        timeSinceAttack += Time.deltaTime;

        Attack();
        Block();

        if(!isBlocking)
        {
            movePlayer();
        }
        else
        {
            //if blocking cant move
        }

        if(isAttacking)
        {
            inputHandle.move = Vector2.zero;
        }
        else
        {
            //if not attacking can move
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
                    StartCoroutine(cdTime());
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
        float forwardSpeed = 3.5f;

        rb.AddForce(transform.forward * forwardSpeed, ForceMode.Impulse);

        //StartCoroutine(setZeroAtkFw());
    }

    private void Block()
    {
        if(inputHandle.block && stats.currentBlocked > 0)
        {
            timeSinceBlock += Time.deltaTime;
            isBlocking = true;
            
            if(timeSinceBlock < 0.3f)
            {
                isParried = true;
            }
            else
            {
                isParried = false;
            }
            
        }
        else
        {
            timeSinceBlock = 0;
            isParried = false;
            isBlocking = false;
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

    private IEnumerator cdTime()
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
