using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;


public class CameraHandler : MonoBehaviour
{
    public Camera cam;
    [SerializeField] private CinemachineFreeLook simple;
    [SerializeField] private CinemachineFreeLook zoom;
    [SerializeField] private CinemachineFreeLook flight;
    [SerializeField] private GameObject reticle;
    [SerializeField] private GameObject PoI;

    private Gravity gravity;

    private void Awake()
    {
        cam = Camera.main;
        gravity = transform.parent.GetComponentInChildren<Gravity>();
        //reticle.SetActive(false);
    }

    //public void _OnRange(InputAction.CallbackContext context) {
    //    zoom.Priority = InputManager.ranged ? 12 : 9;
    //    reticle.SetActive(InputManager.ranged);
    //}

    public void Update()
    {
        flight.Priority = gravity.floating ? 11 : 9;
        zoom.Priority = InputManager.ranged ? 12 : 9;
        //reticle.SetActive(InputManager.ranged);

        
    }


}
