using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAnimation : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    UnityEngine.AI.NavMeshAgent navAi;

    public Collider DamageCollider;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody>();
        navAi = GetComponentInParent<UnityEngine.AI.NavMeshAgent>();

        DamageCollider.enabled = false;
    }

    void Update()
    {
        
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
        DamageCollider.enabled = true;
    }

    public void DisabledDamageCollider()
    {
        DamageCollider.enabled = false;
    }
#endregion
}
