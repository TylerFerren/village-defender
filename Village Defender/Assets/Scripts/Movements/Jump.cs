using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour, IMovementModifier
{
    private MovementHandler movementHandler = null;
    private CharacterController controller;
    void Awake()
    {
        movementHandler = GetComponent<MovementHandler>();
        controller = movementHandler.controller;
    }

    public Vector3 movementVector { get; private set; }

    private void OnEnable() => movementHandler.AddMovementModifer(this);
    private void OnDisable() => movementHandler.RemoveMovementModifer(this);

    void Update() => jump();

    [Header("Settings")]
    [SerializeField] private float JumpHieght = 6f;
    [SerializeField] private float JumpCount = 1f;
    private float jumpCount;
    private float jumpForce;

    public void _OnJump(InputAction.CallbackContext context) {
        if (context.started && jumpCount < JumpCount)
        {
            jumpForce = JumpHieght - Physics.gravity.y;
            jumpCount++;
        }
    }

    void jump() {
        
        if (jumpForce > 0) { jumpForce += Physics.gravity.y * Time.deltaTime;}

        if (jumpForce < 0 && controller.isGrounded) { jumpForce = 0; jumpCount = 0; }

        movementVector = new Vector3(0, jumpForce, 0);
    }


}
