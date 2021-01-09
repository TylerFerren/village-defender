using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flight : MonoBehaviour, IMovementModifier
{

    private CharacterController controller = null;
    private MovementHandler movementHandler = null;
    private Camera cam;
    private Gravity gravity;

    void Awake()
    {
        movementHandler = GetComponent<MovementHandler>();
        controller = movementHandler.controller;
        cam = movementHandler.cam;
        gravity = GetComponent<Gravity>();
    }

    #region MovementModifer

    public Vector3 movementVector { get; private set; }

    private void OnEnable() => movementHandler.AddMovementModifer(this);
    private void OnDisable() => movementHandler.RemoveMovementModifer(this);

    #endregion


    [Header("Settings")]
    [SerializeField] private float flightSpeed = 10f;
    [SerializeField] private float acceleration = 1f;

    
    private Vector2 movementInput;
    private float inputSpeed, topSpeed, playerSpeed, moveSpeed;
    private Vector3 movementSmoothing, movementDirection;
    private bool descent;

    void Update() {
        if (controller.isGrounded) {
            movementVector = Vector3.Lerp(movementVector, Vector3.zero, 0.5f * Time.deltaTime);
        }
        flightState();
    }
    private void flightState()
    {
        if (gravity.floating)
        {
            //inputSpeed = !InputManager.sprinting ? flightSpeed : topSpeed;
            if (InputManager.sprinting)
            {
                forwardFlight();
            }
            else { freeMove(); }
        }
    }

    public void _OnJump(InputAction.CallbackContext context) {
        if (!controller.isGrounded && context.started)
        { gravity.floating = true; }
        if (context.performed)
        {
            descent = true;
            StartCoroutine(descending());
        }
        if (context.canceled)
        {
            descent = false;
        }
    }


    private void freeMove()
    {
        movementInput = InputManager.moveVector;

        Vector3 forwardVector = Vector3.Cross(cam.transform.right, Vector3.up);

        if (gravity.floating)
        {
            movementDirection = (movementInput.x * cam.transform.right) + (movementInput.y * forwardVector);
        }
        else { movementDirection = Vector3.zero;}

        movementSmoothing = Vector3.Lerp(movementSmoothing, movementDirection, Time.deltaTime * acceleration);

        moveSpeed = Mathf.Lerp(moveSpeed, flightSpeed * movementDirection.magnitude, Time.deltaTime * acceleration);
        if (moveSpeed < 0.001f) moveSpeed = 0;

        if (movementDirection.magnitude != 0f) { rotate(); }

        movementVector = (movementSmoothing * moveSpeed);
    }

    private void forwardFlight()
    {
        movementInput = InputManager.moveVector;

        Vector3 upwardVector = Vector3.Cross(-cam.transform.right, Vector3.forward);

        if (gravity.floating)
        {
            movementDirection = (movementInput.x * cam.transform.right) + (movementInput.y * upwardVector) ;
        }
        else { movementDirection = Vector3.zero; }

        movementSmoothing = Vector3.Lerp(movementSmoothing, movementDirection , Time.deltaTime * acceleration);

        moveSpeed = Mathf.Lerp(moveSpeed, flightSpeed * movementDirection.magnitude, Time.deltaTime * acceleration);
        if (moveSpeed < 0.001f) moveSpeed = 0;

        if (movementDirection.magnitude != 0f) { rotate(); }

        movementVector = (movementSmoothing * moveSpeed) + (cam.transform.forward * flightSpeed);
    }



    private void rotate()
    {
        Quaternion rot = controller.transform.rotation;

        Quaternion turnRotation = Quaternion.LookRotation(cam.transform.forward, Vector3.up);

        controller.transform.rotation = Quaternion.Slerp(rot, turnRotation, 5 * Time.deltaTime);
    }

    private IEnumerator descending()
    {
        float i = 0;
        while(descent) {
            movementVector = Vector3.Lerp(movementVector, Vector3.down * -Physics.gravity.y, i * Time.deltaTime);
            i++;
            yield return null;
        }
        movementVector = new Vector3(movementVector.x, 0, movementVector.z);
        yield break;
    }
}
