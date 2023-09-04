using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    Animator anim;
    UnityEngine.AI.NavMeshAgent navAi;
    Rigidbody rb;

    private float delay = 0.1f;

    [Header("A.I. Settings")]
    public float detectionRadius = 5;
    public float moveSpeed;
    public float rotateSpeed;
    public float maximumDetectionAngle = 50;
    public float minimumDetectionAngle = -50;
    public float maximumAggroRadius;
    public float viewableAngle;
    public LayerMask detectionLayer;
    public PlayerController target;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        navAi = GetComponent<UnityEngine.AI.NavMeshAgent>();

        navAi.stoppingDistance = maximumAggroRadius;
    }
 
    void Update()
    {
        IdleState();
    }

    private void IdleState()
    {   
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            PlayerController player = colliders[i].transform.GetComponent<PlayerController>();
            
            if(player != null)
            {
                Vector3 targetDirection = player.transform.position - transform.position;
                viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if(viewableAngle > minimumDetectionAngle && viewableAngle < maximumDetectionAngle)
                {
                    target = player;
                }
            }
        }

        if(target != null)
        {
            PursueTargetState();
        }
        else
        {
            anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        }
    }

    private void PursueTargetState()
    {
        Vector3 targetDirection = target.transform.position - transform.position;
        float distanceFromTarget = Vector3.Distance(target.transform.position, transform.position);
        float viewableAngle = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up); 

        Quaternion rotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed / Time.deltaTime);

        navAi.speed = moveSpeed;

        if(distanceFromTarget <= maximumAggroRadius || target == null)
        {
            anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        }
        else
        {
            navAi.updatePosition = true;
            navAi.isStopped = false;
            navAi.SetDestination(target.transform.position);
            anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            anim.SetInteger("Hit", 0);
        }

        if(distanceFromTarget >= maximumAggroRadius)
        {  
            
        }
        else if(distanceFromTarget <= maximumAggroRadius)
        {
            AttackState();
        }
        else
        {
            PursueTargetState();
        }
    }
    
    private void AttackState()
    {
        navAi.isStopped = true;
    }

    private void Knockback()
    {
        float knockBackForce = 10;

        rb.AddForce(-transform.forward * knockBackForce, ForceMode.Impulse);
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Weapons")
        {
            Knockback();
            StartCoroutine(Reset());
        }    
        else
        {
            
        }
    }
}