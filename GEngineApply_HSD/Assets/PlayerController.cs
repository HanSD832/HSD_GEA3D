using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpPower = 5f;
    public float gravity = -9.81f;

    public CinemachineVirtualCamera virtualCam;
    public float rotationSpeed = 10f;
    private CinemachinePOV pov;
    public CinemachineSwitcher cS;

    private CharacterController controller;
    private Vector3 velocity;
    public bool isGrounded;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        pov = virtualCam.GetCinemachineComponent<CinemachinePOV>();
    }

    void Update()
    {
        //
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //CAM
        Vector3 camForward = virtualCam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = virtualCam.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 move = (camForward * z + camRight * x).normalized;
        if (!cS.usingFreelook)
        {
            if(Input.GetKey(KeyCode.LeftShift))
                controller.Move(move * (speed + 4) * Time.deltaTime);
            else
                controller.Move(move * speed * Time.deltaTime);
        }
        

        float cameraYaw = pov.m_HorizontalAxis.Value;
        Quaternion targetRot = Quaternion.Euler(0f, cameraYaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);

        //JUMP
        if (isGrounded && !cS.usingFreelook && Input.GetKeyDown(KeyCode.Space)) 
        {
            velocity.y = jumpPower;
        }

        //GRAV
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
            virtualCam.m_Lens.FieldOfView = 90f;
        else
            virtualCam.m_Lens.FieldOfView = 60f;
    }
}
