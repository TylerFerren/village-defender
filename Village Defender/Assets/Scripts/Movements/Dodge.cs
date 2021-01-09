using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


public class Dodge : MonoBehaviour, IMovementModifier
{
    public float speed = 0.3f;
    private CharacterController controller = null;
    private MovementHandler movementHandler = null;
    private Camera cam;
    

    void Awake()
    {
        movementHandler = GetComponent<MovementHandler>();
        controller = movementHandler.controller;
        cam = movementHandler.cam;
    }

    #region MovementModifier
    public Vector3 movementVector { get; private set; }

    private void OnEnable() => movementHandler.AddMovementModifer(this);
    private void OnDisable() => movementHandler.RemoveMovementModifer(this);
    #endregion

    private Vector2 movementInput;

    public void _OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>().normalized;
    }

    public void _OnDodge(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) { 
            if (ctx.interaction is TapInteraction) {
                
                StartCoroutine(Locamotion.StopMove(0.7f));

                var destination = controller.transform.forward * -2f;
                StartCoroutine(MoveToPosition(controller.transform, destination, speed));
            }
            if (ctx.interaction is HoldInteraction){
                StartCoroutine(Locamotion.StopMove(1f));

                Vector3 forwardVector = Vector3.Cross(cam.transform.right, Vector3.up);
                var destination = ((movementInput.x * cam.transform.right) + (movementInput.y *  forwardVector)) * 5f;
                if (movementInput != Vector2.zero)
                {
                    StartCoroutine(MoveToPosition(controller.transform, destination, speed));
                }
                else { StartCoroutine(MoveToPosition(controller.transform, controller.transform.forward, speed)); }
            }
        }
    }

  
    public IEnumerator MoveToPosition(Transform transform, Vector3 destination, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, currentPos + destination, t);
            yield return null;
        }
    }

}
