using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class stateMachine : MonoBehaviour
{
    private Locamotion groundMovement = null;
    private Jump jumpMovement = null;
    private Gravity gravity = null;
    private Flight flight = null;
    private CharacterController controller = null;

   [SerializeField] private CinemachineFreeLook locamotionCam;
   [SerializeField] private CinemachineFreeLook flightCam;

    public enum MovementState { locamotion, flight, equipped }
    public MovementState movementState;
    private MovementState perivousState;

    void Awake()
    {
        groundMovement = GetComponentInChildren<Locamotion>();
        jumpMovement = GetComponentInChildren<Jump>();
        gravity = GetComponentInChildren<Gravity>();
        flight = GetComponentInChildren<Flight>();
        controller = GetComponent<CharacterController>();
        
        movementState = MovementState.locamotion;
        perivousState = MovementState.flight;
    }

    
    void Update()
    {
        movementStateSwitcher();
        
        //on StateMachine Change
        if (movementState != perivousState) {
            movementStateHandler();
        }

        perivousState = movementState;

    }

    private void movementStateSwitcher() {
        switch (movementState)
        {
            case MovementState.locamotion:
                
                break;
            case MovementState.flight:
                if (controller.isGrounded)
                { movementState = MovementState.locamotion; }

                break;
            case MovementState.equipped:

                break;
        }
    }

    private void movementStateHandler() {
        switch (movementState)
        {
            case MovementState.locamotion:
                groundMovement.enabled = true;
                //gravity.enabled = true;
                flight.enabled = false;

                flightCam.Priority = 1;
                locamotionCam.Priority = 10;

                break;
            case MovementState.flight:
                groundMovement.enabled = false;
                
                gravity.enabled = false;
                flight.enabled = true;

                flightCam.Priority = 10;
                locamotionCam.Priority = 1;
                break;
        }
    }

    public void jumpInput(InputAction.CallbackContext ctx)
    {
        switch (movementState)
        {
            case MovementState.locamotion:
                if (!controller.isGrounded && ctx.started)
                { movementState = MovementState.flight; }

                break;
            case MovementState.flight:
                if (ctx.started)
                { movementState = MovementState.locamotion; }
                break;
        }
    }

    public void equipInput(InputAction.CallbackContext ctx) {
        switch (movementState)
        {
            case MovementState.locamotion:
                if (ctx.started)
                { movementState = MovementState.equipped; }

                break;
            case MovementState.equipped:
                if (ctx.started)
                { movementState = MovementState.locamotion; }
                break;
        }
    }

}
