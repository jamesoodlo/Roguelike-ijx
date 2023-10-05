using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    InputHandle inputHandle;
    public bool isInteract;
    public bool canInteract;
    public float distanceFromTarget;
    public float detectionRadius = 5;
    public float viewableAngle;
    public float maximumDetectionAngle = 50;
    public float minimumDetectionAngle = -50;
    public LayerMask detectionLayer;
    public Portal portalObj;
    
    void Start()
    {
        inputHandle = GetComponent<InputHandle>();
    }

    void Update()
    {
        isInteract = inputHandle.interaction;
        detectionInteract();
        Interact();
    }

    public void detectionInteract()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            Portal portal = colliders[i].transform.GetComponent<Portal>();
            
            if(portal != null)
            {
                Vector3 targetDirection = portal.transform.position - transform.position;
                viewableAngle = Vector3.Angle(targetDirection, transform.forward);
                distanceFromTarget = Vector3.Distance(portal.transform.position, this.transform.position);

                if(viewableAngle > minimumDetectionAngle && viewableAngle < maximumDetectionAngle)
                {
                    portalObj = portal;
                }
                else
                {
                    portalObj = null;
                }
            }
        }
    }

    public void Interact()
    {
        if(portalObj != null && distanceFromTarget <= 2) 
        {
            canInteract = true;
            portalObj = GetComponent<Portal>();
        }
    }
}
