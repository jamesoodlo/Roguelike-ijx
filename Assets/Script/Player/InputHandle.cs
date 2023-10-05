using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputHandle : MonoBehaviour
{
    public bool attack, dash, block, skillQ, skillE, interaction;
    public Vector2 move, mouseLook;
    public Vector3 rotationTarget;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            attack = true;
        }
        if(context.performed)
        {
            attack = true;
        }
        if(context.canceled)
        {
            attack = false;
        }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            block = true;
        }
        if(context.performed)
        {
            block = true;
        }
        if(context.canceled)
        {
            block = false;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            dash = true;
        }
        if(context.performed)
        {
            dash = true;
        }
        if(context.canceled)
        {
            dash = false;
        }
    }

    public void OnSkillQ(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            skillQ = true;
        }
        if(context.performed)
        {
            skillQ = true;
        }
        if(context.canceled)
        {
            skillQ = false;
        }
    }

    public void OnSkillE(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            skillE = true;
        }
        if(context.performed)
        {
            skillE = true;
        }
        if(context.canceled)
        {
            skillE = false;
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            interaction = true;
        }
        if(context.performed)
        {
            interaction = true;
        }
        if(context.canceled)
        {
            interaction = false;
        }
    }
}
