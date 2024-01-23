using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Min & Max camera view")]
    private const float YMin = -50f;
    private const float YMax = 50f;

    [Header("Camera view")]
    public Transform lookAt;

    [Header("Camera Position")]
    public float cameraDistance = 10f;
    public float scopeCameraDistance = 1.3f;
    private float currentCameraDistance;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    public float cameraSensitivity = 4f;

    public FloatingJoystick floatingJoystick;

    private int clickCount = 0;

    private void Start()
    {
        currentCameraDistance = cameraDistance;
    }

    private void LateUpdate()
    {
        currentX += floatingJoystick.Horizontal * cameraSensitivity * Time.deltaTime;
        currentY -= floatingJoystick.Vertical * cameraSensitivity * Time.deltaTime;

        currentY = Mathf.Clamp(currentY, YMin, YMax);

        Vector3 direction = new Vector3(0, 0, -currentCameraDistance);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        if(lookAt != null)
        {
            transform.position = lookAt.position + rotation * direction;

            transform.LookAt(lookAt.position);
        }    
    }

    public void OnClickScopeMobile()
    {
        if (clickCount % 2 == 0)
        {
            currentCameraDistance = scopeCameraDistance;
        }
        else
        {
            currentCameraDistance = cameraDistance;
        }

        clickCount++;
    }    
}
