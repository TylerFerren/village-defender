using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour, IMovementModifier
{

    private CharacterController controller = null;
    private MovementHandler movementHandler = null;

    #region MovementModifer
    public Vector3 movementVector { get; private set; }

    private void OnEnable() => movementHandler.AddMovementModifer(this);
    private void OnDisable() => movementHandler.RemoveMovementModifer(this);

    [Header("Settings")]
    public bool floating;

    void Awake()
    {
        controller = GetComponentInParent<CharacterController>();
        movementHandler = GetComponent<MovementHandler>();

    }
    #endregion

    private void Update() => gravity();

    private void gravity() {

        if (controller.isGrounded || controller.velocity.y > 0f) { movementVector = Physics.gravity; }
        if (!controller.isGrounded && !floating) {
            movementVector += (Physics.gravity * Time.deltaTime);
        }

        if (floating) {
            if (movementVector.magnitude < 0.01)
                movementVector = Vector3.Lerp(movementVector, Vector3.zero, 2 * Time.deltaTime);
            else movementVector = Vector3.zero;
        }

        if (controller.isGrounded) floating = false;
    }
}
