using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public string changeAngle;
    public Transform target;
    public float smoothTime = 0.3f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        
    }

    
    void Update()
    {
        if(target != null)
        {
            if(changeAngle == "1")
            {
                Quaternion newRotation = Quaternion.Euler(40, 0, 0);

                transform.rotation = newRotation;
                offset = new Vector3(0, 6, -4);
            }
            else if(changeAngle == "2")
            {
                Quaternion newRotation = Quaternion.Euler(60, 0, 0);

                transform.rotation = newRotation;
                offset = new Vector3(0, 12, -5);
            }
            else if(changeAngle == "3")
            {
                Quaternion newRotation = Quaternion.Euler(60, 0, 0);

                transform.rotation = newRotation;
                offset = new Vector3(0, 7.5f, -3);
            }


            Vector3 targetPosition = target.position + offset;

            transform.position =  Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            
        }

        
    }
}
