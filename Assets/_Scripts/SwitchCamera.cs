using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [Header("Camera to Assign")]
    public GameObject camTPC;
    public GameObject camAC;
    public GameObject thirdPersonCam;
    public GameObject AimCam;

    private void Update()
    {
        if(Input.GetButton("Fire2"))
        {
            camTPC.SetActive(false);
            camAC.SetActive(true);
            thirdPersonCam.SetActive(false);
            AimCam.SetActive(true);
        }    
        else
        {
            camTPC.SetActive(true);
            camAC.SetActive(false);
            thirdPersonCam.SetActive(true);
            AimCam.SetActive(false);
        }    
    }
}
