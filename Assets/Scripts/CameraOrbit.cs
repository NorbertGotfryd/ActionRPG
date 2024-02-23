using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform camAnchor;
    public float lookSensitivity;
    public float minXLook;
    public float maxXLook;
    public bool invertXRotation;

    private float currentXRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        //rotate the player horizantlly
        transform.eulerAngles += Vector3.up * x * lookSensitivity;

        if (invertXRotation)
            currentXRotation += y * lookSensitivity;
        else
            currentXRotation -= y * lookSensitivity;

        currentXRotation = Mathf.Clamp(currentXRotation, minXLook, maxXLook);

        Vector3 clampedAngle = camAnchor.eulerAngles;
        clampedAngle.x = currentXRotation;

        camAnchor.eulerAngles = clampedAngle;
    }
}
