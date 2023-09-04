using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimation : MonoBehaviour
{
    Animator anim;
    InputHandle inputHandle;
    
    private string moveDirection;

    private void Start() 
    {
        inputHandle = GetComponentInParent<InputHandle>();
        anim = GetComponent<Animator>();     
    }

    public void moveAnimation()
    {
        if(moveDirection == "X" && inputHandle.move.x == 1)
        {
            anim.SetBool("isForward", true);
            anim.SetBool("isReverse", false);
            anim.SetBool("isStrafeR", false);
            anim.SetBool("isStrafeL", false);
        }
        else if(moveDirection == "X" && inputHandle.move.x == -1)
        {
            anim.SetBool("isForward", false);
            anim.SetBool("isReverse", true);
            anim.SetBool("isStrafeR", false);
            anim.SetBool("isStrafeL", false);
        }
        else if(moveDirection == "X" && inputHandle.move.y == 1)
        {
            anim.SetBool("isForward", false);
            anim.SetBool("isReverse", false);
            anim.SetBool("isStrafeL", true);
            anim.SetBool("isStrafeR", false);       
        }
        else if(moveDirection == "X" && inputHandle.move.y == -1)
        {
            anim.SetBool("isForward", false);
            anim.SetBool("isReverse", false);
            anim.SetBool("isStrafeL", false);
            anim.SetBool("isStrafeR", true);
        }
        else if(moveDirection == "-X" && inputHandle.move.x == 1)
        {
            anim.SetBool("isForward", false);
            anim.SetBool("isReverse", true);
            anim.SetBool("isStrafeR", false);
            anim.SetBool("isStrafeL", false);
        }
        else if(moveDirection == "-X" && inputHandle.move.x == -1)
        {
            anim.SetBool("isForward", true);
            anim.SetBool("isReverse", false);
            anim.SetBool("isStrafeR", false);
            anim.SetBool("isStrafeL", false);
        }
        else if(moveDirection == "-X" && inputHandle.move.y == 1)
        {
            anim.SetBool("isForward", false);
            anim.SetBool("isReverse", false);
            anim.SetBool("isStrafeL", false);
            anim.SetBool("isStrafeR", true);   
        }
        else if(moveDirection == "-X" && inputHandle.move.y == -1)
        {
            anim.SetBool("isForward", false);
            anim.SetBool("isReverse", false);
            anim.SetBool("isStrafeL", true);
            anim.SetBool("isStrafeR", false);
        }
        else if(moveDirection == "Z" && inputHandle.move.y == 1)
        {
            anim.SetBool("isForward", true);
            anim.SetBool("isReverse", false);
            anim.SetBool("isStrafeR", false);
            anim.SetBool("isStrafeL", false);
        }
        else if(moveDirection == "Z" && inputHandle.move.y == -1)
        {
            anim.SetBool("isForward", false);
            anim.SetBool("isReverse", true);
            anim.SetBool("isStrafeR", false);
            anim.SetBool("isStrafeL", false);
        }
        else if(moveDirection == "Z" && inputHandle.move.x == 1)
        {
            anim.SetBool("isForward", false);
            anim.SetBool("isReverse", false);
            anim.SetBool("isStrafeL", false);
            anim.SetBool("isStrafeR", true);       
        }
        else if(moveDirection == "Z" && inputHandle.move.x == -1)
        {
            anim.SetBool("isForward", false);
            anim.SetBool("isReverse", false);
            anim.SetBool("isStrafeL", true);
            anim.SetBool("isStrafeR", false);
        }
        else if(moveDirection == "-Z" && inputHandle.move.y == 1)
        {
            anim.SetBool("isForward", false);
            anim.SetBool("isReverse", true);
            anim.SetBool("isStrafeR", false);
            anim.SetBool("isStrafeL", false);
        }
        else if(moveDirection == "-Z" && inputHandle.move.y == -1)
        {
            anim.SetBool("isForward", true);
            anim.SetBool("isReverse", false);
            anim.SetBool("isStrafeR", false);
            anim.SetBool("isStrafeL", false);
        }
        else if(moveDirection == "-Z" && inputHandle.move.x == 1)
        {
            anim.SetBool("isForward", false);
            anim.SetBool("isReverse", false);
            anim.SetBool("isStrafeL", true);
            anim.SetBool("isStrafeR", false);   
        }
        else if(moveDirection == "-Z" && inputHandle.move.x == -1)
        {
            anim.SetBool("isForward", false);
            anim.SetBool("isReverse", false);
            anim.SetBool("isStrafeL", false);
            anim.SetBool("isStrafeR", true);
        }
    }

    public void facingDirection()
    {
        Vector3 playerForwardDirection = transform.forward;

        playerForwardDirection.y = 0;

        playerForwardDirection.Normalize();

        float dotX = Vector3.Dot(playerForwardDirection, Vector3.right);
        float dotZ = Vector3.Dot(playerForwardDirection, Vector3.forward);

        float threshold = 0.8f;

        if (dotX > threshold)
        {
            moveDirection = "X";
        }
        else if (dotX < -threshold)
        {
            moveDirection = "-X";
        }
        else if (dotZ > threshold)
        {
            moveDirection = "Z";
        }
        else if (dotZ < -threshold)
        {
            moveDirection = "-Z";
        }
    }
}
