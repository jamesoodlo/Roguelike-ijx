using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandle : MonoBehaviour
{
    Animator anim;
    InputHandle inputHandle;
    PlayerController playerControll;
    PlayerStats stats;
    Rigidbody rb;
    SoundFx soundFx;

    public Collider playerCollider;
    public GameObject[] slashObj;

    void Start()
    {
        playerControll = GetComponent<PlayerController>();
        inputHandle = GetComponent<InputHandle>();
        soundFx = GetComponent<SoundFx>();
        stats = GetComponent<PlayerStats>();
        anim = GetComponent<Animator>(); 
        rb = GetComponent<Rigidbody>();

        DisabledSlashFx();
        DisabledSlashFx2();
        DisabledSlashFx3();
    }

    private void Update() 
    {
        moveAnimation();
        
        if(stats.currentGuard <= 0)
        {
            anim.SetBool("Block", false);
        }
        else
        {
            anim.SetBool("Block", playerControll.isGuard);
        }
    }

    public void moveAnimation()
    {
        if(inputHandle.move != Vector2.zero)
        {
            anim.SetFloat("Horizontal", 1, 0.1f, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Horizontal", 0);
        }
    }

    public void AttackAnimation()
    {
        anim.SetTrigger("Attack" + playerControll.currentAttack);
    }

    public void RollAnimation()
    {
        anim.SetTrigger("isRolling");
    }

    public void SuperDuckAnimation()
    {
        anim.SetTrigger("Skill2");
    }

#region AnimationEvent
    public void EnabledDamageCollider()
    {
        Weapons weapon;
        weapon = GetComponentInChildren<Weapons>();
        weapon.onAttack = true;
    }

    public void DisabledDamageCollider()
    {
        Weapons weapon;
        weapon = GetComponentInChildren<Weapons>();
        weapon.onAttack = false;
    }

    public void ResetAttack()
    {
        playerControll.isAttacking = false;
    } 

    public void startHurt()
    {
        stats.isHurt = true;
    }

    public void endHurt()
    {
        stats.isHurt = false;
    }

    public void DrainStamina()
    {
        if(!playerControll.isSuperDuck) stats.currentStamina -= 15;
    }

    public void FootStepSfx()
    {
        soundFx.footStepSfx.Play();
    }

    public void SlashSfx()
    {
        soundFx.slashSfx.Play();
    }

    public void RollSfx()
    {
        soundFx.rollingSfx.Play();
    }

    public void BuffSfx()
    {
        soundFx.buffSfx.Play();
    }

    public void EnebledSlashFx()
    {

        slashObj[0].SetActive(true);
    }

    public void EnebledSlashFx2()
    {
        slashObj[1].SetActive(true);
    }

    public void EnebledSlashFx3()
    {
        slashObj[2].SetActive(true);
    }

    public void DisabledSlashFx()
    {
        slashObj[0].SetActive(false);
    }

    public void DisabledSlashFx2()
    {
        slashObj[1].SetActive(false);
    }

    public void DisabledSlashFx3()
    {
        slashObj[2].SetActive(false);
    }

    public void EnabledPlayerCollider()
    {
        playerCollider.enabled = true;
    }

    public void DisabledPlayerCollider()
    {
        playerCollider.enabled = false;
    }

    public void DontMove()
    {
        inputHandle.move = Vector2.zero;
    }

    public void EnebledSlashSkill()
    {
        if(playerControll.isSlash) playerControll.onSlash = true;
    }

    public void DisabledSlashSkill()
    {
        playerControll.onSlash = false;
    }
#endregion
}
