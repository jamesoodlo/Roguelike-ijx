using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyMelee : MonoBehaviour
{
    Animator anim;
    UnityEngine.AI.NavMeshAgent navAi;
    Rigidbody rb;
    private GameObject findPlayer;
    private  PlayerController playerObj;

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
    

    [Header("Combat")]
    public GameObject textParry;
    public bool isParried;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        navAi = GetComponent<UnityEngine.AI.NavMeshAgent>();

        textParry.SetActive(false);
        navAi.stoppingDistance = maximumAggroRadius;

        findPlayer = GameObject.Find("Player");
        playerObj = findPlayer.GetComponent<PlayerController>();

        StartCoroutine(SetTarget());
    }
 
    void Update()
    {
        IdleState();

        if(isParried)
        {
            textParry.SetActive(true);
        }
        else
        {
            textParry.SetActive(false);
        }
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
        float distanceFromTarget = Vector3.Distance(target.transform.position, this.transform.position);
        float viewableAngle = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up); 

        Quaternion rotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed / Time.deltaTime);

        navAi.speed = moveSpeed;

        if(target == null)
        {
            anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        }
        else if(distanceFromTarget <= maximumAggroRadius)
        {
            AttackState();
        }
        else
        {
            navAi.updatePosition = true;
            navAi.isStopped = false;
            navAi.SetDestination(target.transform.position);
            anim.SetInteger("Hit" , 0);
            anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
        }
    }
    
    private void AttackState()
    {
        navAi.isStopped = true;
        anim.SetInteger("Hit" , 1);
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Hit1"))
        {
            anim.SetInteger("Hit", 2);
        }

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Hit2"))
        {
            anim.SetInteger("Hit", 1);
        }
    }

    public void Knockback()
    {
        float knockBackForce = 10;
        
        rb.AddForce(-transform.forward * knockBackForce, ForceMode.Impulse);

        StartCoroutine(Reset());
    }

    IEnumerator Reset(float delay = 0.1f)
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector3.zero;
    }

    IEnumerator SetTarget(float delay = 1.5f)
    {
        yield return new WaitForSeconds(delay);
        target = playerObj;
    }
}
