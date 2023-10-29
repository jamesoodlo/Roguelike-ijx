using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAnimation : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    EnemyMelee enemyMelee;
    UnityEngine.AI.NavMeshAgent navAi;
    EnemyWeapon enemyWeapon;
    SoundFx soundFx;

    public GameObject slashObj;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody>();
        enemyMelee = GetComponentInParent<EnemyMelee>();
        navAi = GetComponentInParent<UnityEngine.AI.NavMeshAgent>();
        enemyWeapon = GetComponentInChildren<EnemyWeapon>();
        soundFx = GetComponentInParent<SoundFx>();

        enemyWeapon.onAttack = false;

        DisabledSlashFx();
    }

    void Update()
    {
        if(enemyMelee.isParried)
        {
            DisabledDamageCollider();
        }
    }

#region Animation Event
    public void AttackingDontMove()
    {
        navAi.enabled = false;
        navAi.isStopped = true;
    }

    public void AttackedCanMove()
    {
        navAi.enabled = true;
        navAi.isStopped = false;
    }

    public void EnabledDamageCollider()
    {
        enemyWeapon.onAttack = true;
    }

    public void DisabledDamageCollider()
    {
        enemyWeapon.onAttack = false;
    }

    public void EndParried()
    {
        enemyMelee.isParried = false;
    }

    public void EnebledSlashFx()
    {
        slashObj.SetActive(true);
    }

    public void DisabledSlashFx()
    {
        slashObj.SetActive(false);
    }


    public void SlashSfx()
    {
        soundFx.slashSfx.Play();
    }

    public void FootStepSfx()
    {
        soundFx.footStepSfx.Play();
    }
#endregion
}
