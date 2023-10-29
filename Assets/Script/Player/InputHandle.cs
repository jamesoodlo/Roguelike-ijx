using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputHandle : MonoBehaviour
{
    public bool attack, dash, guard, skillQ, skillE, interaction, item1, item2;
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

    public void OnGuard(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            guard = true;
        }
        if(context.performed)
        {
            guard = true;
        }
        if(context.canceled)
        {
            guard = false;
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

    public void OnItem1(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            item1 = true;
        }
        if(context.performed)
        {
            item1 = true;
        }
        if(context.canceled)
        {
            item1 = false;
        }
    }

    public void OnItem2(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            item2 = true;
        }
        if(context.performed)
        {
            item2 = true;
        }
        if(context.canceled)
        {
            item2 = false;
        }
    }
}
