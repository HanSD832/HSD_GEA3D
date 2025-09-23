using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera virtualcam;
    public CinemachineFreeLook freeLookCam;
    public bool usingFreelook = false;
    // Start is called before the first frame update
    void Start()
    {
        virtualcam.Priority = 10;
        freeLookCam.Priority = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            usingFreelook = !usingFreelook;
            if(usingFreelook)
            {
                freeLookCam.Priority = 20;
                virtualcam.Priority = 0;
            }
            else
            {
                virtualcam.Priority = 20;
                freeLookCam.Priority = 0;
            }
        }
    }
}
