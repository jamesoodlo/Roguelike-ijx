using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class UIInput : MonoBehaviour
{
    public bool escape;

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
}
