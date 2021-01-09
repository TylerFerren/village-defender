using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class simpleMovement : MonoBehaviour
{
    
    [SerializeField] private CharacterController controller;
    
    [SerializeField] private Camera cam;
    
    
    private Vector2 movementInput = new Vector2(0f,0f);
    private Vector3 movementDirection;


    [SerializeField] private float moveSpeed = 0, playerSpeed = 6f, jumpForce = 6, verticalInput;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!controller.isGrounded) { verticalInput += Physics.gravity.y * Time.deltaTime; }
        movementDirection = ((movementInput.x * cam.transform.right) + (movementInput.y * cam.transform.forward) + (Vector3.up * verticalInput));
        moveSpeed = Mathf.Lerp(moveSpeed, playerSpeed, 2 * Time.deltaTime);
        controller.Move(movementDirection * moveSpeed * Time.deltaTime);
    }

    public void moveInput(InputAction.CallbackContext ctx) {

        movementInput = ctx.ReadValue<Vector2>().normalized;
     
    }

    public void jumpInput(InputAction.CallbackContext ctx) {

        if (ctx.started && controller.isGrounded) { verticalInput = jumpForce; }


    }

}
