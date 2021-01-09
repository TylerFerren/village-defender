using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementHandler : MonoBehaviour
{
    [Header ("refrences")]
    public CharacterController controller = null;
    public Camera cam = null;

    void Awake()
    {
        controller = GetComponentInParent<CharacterController>();
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        move();
    }

    public Vector3 Velocity;

    private readonly List<IMovementModifier> movementModifiers = new List<IMovementModifier>();

    public void AddMovementModifer(IMovementModifier movementModifier) => movementModifiers.Add(movementModifier);

    public void RemoveMovementModifer(IMovementModifier movementModifier) => movementModifiers.Remove(movementModifier);

    void move()
    {
        Vector3 movement = Vector3.zero;

        foreach (IMovementModifier movementModifier in movementModifiers) {
            movement += movementModifier.movementVector;
        }
        
        controller.Move(movement * Time.deltaTime);

        Velocity = controller.velocity;
    }
}
