using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class Locamotion : MonoBehaviour, IMovementModifier, IUseEnergy
{
    private CharacterController controller = null;
    private MovementHandler movementHandler = null;
    private PlayerManager playerManager = null;
    private Camera cam;
    private Gravity gravity;

    void Awake()
    {
        movementHandler = GetComponent<MovementHandler>();
        controller = movementHandler.controller;
        playerManager = GetComponentInParent<PlayerManager>();
        cam = movementHandler.cam;
        gravity = GetComponent<Gravity>();
        inputSpeed = walkSpeed;
    }

    #region MovementModifer

    public Vector3 movementVector { get; private set; }

   

    private void OnEnable() => movementHandler.AddMovementModifer(this);
    private void OnDisable() => movementHandler.RemoveMovementModifer(this);

    #endregion

    #region IUseEnergy
    public float energyCost { get => sprintEnergy; set => sprintEnergy = value; }
    protected Energy.EnergyUseType EnergyUseType = Energy.EnergyUseType.instant;
    public Energy.EnergyUseType energyUseType { get => EnergyUseType; set => EnergyUseType = value; }
    protected bool isUsingEnergy;
    public bool usingEnergy { get => isUsingEnergy; set => isUsingEnergy = value; }
    #endregion

    [Header("Settings")]
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float sprintSpeed = 12f;
    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float rotation = 4f;
    [SerializeField] private float sprintEnergy = 2f;

    void Update() => move();

    private Vector2 movementInput;
    private float moveSpeed, inputSpeed;
    private Vector3 movementSmoothing, movementDirection, forwardVector;

    
    private void move()
    {
       
        movementInput = InputManager.moveVector;

        forwardVector = Vector3.Cross(cam.transform.right, Vector3.up);

        if (!movementPaused && !gravity.floating)
        {
            movementDirection = ((movementInput.x * cam.transform.right) + (movementInput.y * forwardVector));
            speed();

        }
        else { movementDirection = Vector3.zero; }

        movementSmoothing = Vector3.Lerp(movementSmoothing, movementDirection, Time.deltaTime);
            if (movementSmoothing.magnitude < 0.01f) movementSmoothing = Vector3.zero ;
        

        if (movementSmoothing != Vector3.zero) { 
            rotate();
        }
    
        movementVector = movementSmoothing * moveSpeed;
    }

    private void speed() {
        if (movementDirection.magnitude > 0 | moveSpeed > 0.01f)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, inputSpeed * movementDirection.magnitude, Time.deltaTime * acceleration);
        }
        else
        moveSpeed = 0;
    }

    private void rotate()
    {
        Quaternion rot = controller.transform.rotation;
        Quaternion turnRotation = Quaternion.LookRotation(forwardVector, Vector3.up);
        controller.transform.rotation = Quaternion.Slerp(rot, turnRotation, rotation * Time.deltaTime);
    }

    [HideInInspector] public static bool movementPaused = false;
    public static IEnumerator StopMove(float time)
    {
        movementPaused = true;
        yield return new WaitForSeconds(time);
        movementPaused = false;
    }

    public void _OnSprint(InputAction.CallbackContext ctx){
        if (ctx.performed) {
            isUsingEnergy = true;
            playerManager.energy.useEnergy(this);
            inputSpeed = sprintSpeed;
        }
        if (ctx.canceled) {
            isUsingEnergy = false;
            inputSpeed = walkSpeed;
        }
    }
}
