using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    Animator anim;
    UnityEngine.AI.NavMeshAgent navAi;
    Rigidbody rb;
    EnemyGun gun;
    private GameObject findPlayer;
    private  PlayerController playerObj;

    private float timeSinceDodge;

    public bool isMoveing = false;

    [Header("A.I. Settings")]
    public float detectionRadius = 5;
    public float moveSpeed;
    public float rotateSpeed;
    public float maximumDetectionAngle = 50;
    public float minimumDetectionAngle = -50;
    public float maximumAggroRadius;
    public float dodgingDistance;
    public float viewableAngle;
    public LayerMask detectionLayer;
    public PlayerController target;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gun = GetComponentInChildren<EnemyGun>();
        anim = GetComponentInChildren<Animator>();
        navAi = GetComponent<UnityEngine.AI.NavMeshAgent>();

        navAi.stoppingDistance = maximumAggroRadius;

        findPlayer = GameObject.Find("Player");
        playerObj = findPlayer.GetComponent<PlayerController>();

        StartCoroutine(SetTarget());
    }
 
    void Update()
    {
        IdleState();
        ReloadState();
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
            navAi.enabled = true;
            navAi.updatePosition = true;
            navAi.isStopped = false;
            navAi.SetDestination(target.transform.position);
            anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            isMoveing = true;
            gun.isFiring = false;
            anim.SetInteger("Hit", 0);
        }

        if(distanceFromTarget <= dodgingDistance)
        {  
            timeSinceDodge += Time.deltaTime;

            if(timeSinceDodge >= 1) 
            {
                if(!gun.isReloading)
                {
                    isMoveing = true;
                    gun.isFiring = false;
                    //navAi.enabled = false;
                    navAi.updatePosition = false;
                    navAi.isStopped = true;

                    DodgingBack();
                }
                else
                {
                    timeSinceDodge = 0;
                }
                
            }
            
        }
        else if(distanceFromTarget <= maximumAggroRadius)
        {
            FiringState();
        }
        else
        {
            isMoveing = true;
            gun.isFiring = false;
            navAi.enabled = true;
            navAi.updatePosition = true;
            navAi.isStopped = false;
            //PursueTargetState();
        }
    }
    
    private void FiringState()
    {
        isMoveing = false;
        navAi.enabled = false;

        if(gun.ammo > 0)
        {
            gun.Fire();
        }
    }

    public void ReloadState()
    {
        if(gun.ammo <= 0) gun.Reload();

        if(gun.isReloading)
        {
            navAi.enabled = false;
        }
        else
        {
            navAi.enabled = true;
            navAi.updatePosition = true;
        }
    }

    public void DodgingBack()
    {
        float dodgingForce = 5;

        rb.AddForce(-transform.forward * dodgingForce, ForceMode.VelocityChange);

        gun.isFiring = false;
        navAi.enabled = false;

        anim.Play("Dodging");

        StartCoroutine(ResetDodgingBack());

        timeSinceDodge = 0;
    }

    public void Knockback()
    {
        float knockBackForce = 10;

        rb.AddForce(-transform.forward * knockBackForce, ForceMode.Impulse);

        StartCoroutine(ResetKnockback());
    }

    IEnumerator ResetDodgingBack(float delay = 0.5f)
    {
        yield return new WaitForSeconds(delay);
        gun.isFiring = false;
        navAi.enabled = true;
        navAi.updatePosition = true;
        rb.velocity = Vector3.zero;
    }

    IEnumerator ResetKnockback(float delay = 0.1f)
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector3.zero;
    }

    IEnumerator SetTarget(float delay = 1.5f)
    {
        yield return new WaitForSeconds(delay);
        target = playerObj;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            rb.velocity = Vector3.zero;
        }    
        else
        {
            
        }
    }
}
