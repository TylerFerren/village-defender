
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationParameters : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private CharacterController controller;

    private Locamotion locam;
    private Camera cam;
   

    void Awake()
    {
        anim = GetComponentInParent<Animator>();
        controller = GetComponentInParent<CharacterController>();
        cam = Camera.main;
        locam = GetComponent<Locamotion>();

    }

    void Update() {
        animationControlls();
        groundCheck();
        //inputDirection();
    }

    float landTime;
    void groundCheck()
    {
        if (!controller.isGrounded)
        {
            Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 200);
            float groundDistance = hitInfo.distance;
            landTime = groundDistance / Mathf.Abs(controller.velocity.y);
        }
        else { landTime = 0; }
    }

    public Vector3 locVel;
    private Vector3 direction;

    void animationControlls() {

        locVel = transform.InverseTransformDirection(locam.movementVector);

        anim.SetFloat("M_Mag", new Vector3(locVel.x, 0, locVel.z).magnitude);

        anim.SetFloat("M_Sideways", locVel.x);
        anim.SetFloat("M_Vertical", locVel.y);
        anim.SetFloat("M_Forward", locVel.z);

        anim.SetFloat("Input_Forward", direction.z);
        anim.SetFloat("Input_Sideways", direction.x);

        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("landTime", landTime);

    }

    //private void inputDirection() {

    //    Vector3 cameraCrossproduct = Vector3.Cross(cam.transform.right, Vector3.up);

    //    Quaternion facingRot = Quaternion.Euler(0, Vector3.SignedAngle(controller.transform.forward, cameraCrossproduct, Vector3.up), 0);

    //    Vector3 directinalInput = facingRot * new Vector3(GM.movementInput.x, 0, GM.movementInput.y);

    //    direction = Vector3.Slerp(direction, directinalInput, 3 * Time.deltaTime);
    //}

}
