using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [HideInInspector] public Cinemachine.CinemachineVirtualCamera playerCamera;
    public float verticalRecoil;

    public void GenerateRecoil()
    {
        playerCamera.m_Lens.FieldOfView -= verticalRecoil;
        Debug.Log(playerCamera.m_Lens.FieldOfView);
    }    
}
