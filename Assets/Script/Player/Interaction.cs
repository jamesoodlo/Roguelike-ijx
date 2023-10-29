using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    InputHandle inputHandle;
    SoundFx soundFx;
    public bool isInteract;
    public bool canInteract;
    public float distanceFromTarget;
    public float detectionRadius = 5;
    public float viewableAngle;
    public float maximumDetectionAngle = 50;
    public float minimumDetectionAngle = -50;
    public LayerMask detectionLayer;
    public Items items;
    
    void Start()
    {
        inputHandle = GetComponent<InputHandle>();
        soundFx = GetComponent<SoundFx>();
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
            Items item = colliders[i].transform.GetComponent<Items>();
            
            if(item != null)
            {
                Vector3 targetDirection = item.transform.position - transform.position;
                viewableAngle = Vector3.Angle(targetDirection, transform.forward);
                distanceFromTarget = Vector3.Distance(item.transform.position, this.transform.position);

                if(viewableAngle > minimumDetectionAngle && viewableAngle < maximumDetectionAngle)
                {
                    items = item;
                }
                else
                {
                    items = null;
                }
            }
            else
            {
                distanceFromTarget = 0;
            }
        }
    }

    public void Interact()
    { 
        if(items != null && distanceFromTarget <= 2) 
        {
            canInteract = true;
            
            items = GetComponent<Items>();

            if(canInteract && isInteract) soundFx.pickUpSfx.Play();
        }
        else
        {
            canInteract = false;
        }
    }
}
