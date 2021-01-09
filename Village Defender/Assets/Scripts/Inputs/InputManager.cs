using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    public static Vector2 moveVector;
    public static void _OnMove(InputAction.CallbackContext context) { }
    public void onMove(InputAction.CallbackContext context) {
        BroadcastMessage("_OnMove", context, SendMessageOptions.DontRequireReceiver);
        moveVector = context.ReadValue<Vector2>();
    }

    public static Vector2 lookVector;
    public static void _OnLook(InputAction.CallbackContext context) { }
    public void onLook(InputAction.CallbackContext context)
    {
        BroadcastMessage("_OnLook", context, SendMessageOptions.DontRequireReceiver);
        lookVector = context.ReadValue<Vector2>();
    }

    public static void _OnAttackLight(InputAction.CallbackContext context) { }
    public static void _OnAttackHeavy(InputAction.CallbackContext context) { }
    public void onAttack(InputAction.CallbackContext context)
    {
        if (context.interaction is TapInteraction)
        {
            BroadcastMessage("_OnAttackLight", context, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            BroadcastMessage("_OnAttackHeavy", context, SendMessageOptions.DontRequireReceiver);
        }
    }

    public static void _OnJump(InputAction.CallbackContext context) { }
    public void onJump(InputAction.CallbackContext context)
    {
        BroadcastMessage("_OnJump", context, SendMessageOptions.DontRequireReceiver);
    }

    public static bool sprinting;
    public static void _OnSprint(InputAction.CallbackContext context) { }
    public void onSprint(InputAction.CallbackContext context)
    {
        BroadcastMessage("_OnSprint", context, SendMessageOptions.DontRequireReceiver);
        sprinting = context.action.triggered;
    }

    public static void _OnDodge(InputAction.CallbackContext context) { }
    public void onDodge(InputAction.CallbackContext context)
    {
        BroadcastMessage("_OnDodge", context, SendMessageOptions.DontRequireReceiver);
    }

    public static void _OnPrimaryAbility(InputAction.CallbackContext context) { }
    public void onPrimaryAttack(InputAction.CallbackContext context)
    {
        BroadcastMessage("_OnPrimaryAbility", context, SendMessageOptions.DontRequireReceiver);
    }

    public static void _OnSecondaryAbility(InputAction.CallbackContext context) { }
    public void onSecondaryAttack(InputAction.CallbackContext context)
    {
        BroadcastMessage("_OnSecondaryAbility", context, SendMessageOptions.DontRequireReceiver);
    }

    public static void _OnTertiaryAbility(InputAction.CallbackContext context) { }
    public void onTertiaryAttack(InputAction.CallbackContext context)
    {
        BroadcastMessage("_OnTertiaryAbility", context, SendMessageOptions.DontRequireReceiver);
    }

    public static bool ranged;
    public static void _OnRange(InputAction.CallbackContext context) { }
    public void onRange(InputAction.CallbackContext context)
    {
        BroadcastMessage("_OnRange", context, SendMessageOptions.DontRequireReceiver);
        if (context.started) ranged = true;
        if (context.canceled) ranged = false;
    }

}
