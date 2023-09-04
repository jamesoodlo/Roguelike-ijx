using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputHandle : MonoBehaviour
{
    public bool attack, dash, escape, block, skillQ, skillE, skillR, item1, item2, item3;
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

    public void OnEscape(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            escape = true;
        }
        if(context.performed)
        {
            escape = true;
        }
        if(context.canceled)
        {
            escape = false;
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

    public void OnSkillR(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            skillR = true;
        }
        if(context.performed)
        {
            skillR = true;
        }
        if(context.canceled)
        {
            skillR = false;
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

    public void OnItem3(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            item3 = true;
        }
        if(context.performed)
        {
            item3 = true;
        }
        if(context.canceled)
        {
            item3 = false;
        }
    }
}
