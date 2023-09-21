using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOrbFollow : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent navAi;
    public PlayerController target;
    private float timeSinceHeal;
    
    void Start()
    {
        navAi = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if(target != null)
        {
            timeSinceHeal += Time.deltaTime;

            if(timeSinceHeal >= 10.0f)
            {
                Destroy(this.gameObject);
            }

            navAi.speed = target.speed - 1;
            navAi.updatePosition = true;
            navAi.isStopped = false;
            navAi.SetDestination(target.transform.position);
        }

        
    }
}
