using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSens = 100f;
    public bool invertX, invertY;
    [SerializeField] Transform camParent;
    [SerializeField] bool startLocked;
    float xRotation = 0f;

    private void Start()
    {
        if (startLocked)
            LockMouse();
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSens;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSens;

        if (invertX)
            mouseX = -mouseX;

        if (!invertY)
            xRotation -= mouseY;
        else if (invertY)
            xRotation += mouseY;

        //xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        camParent.Rotate(Vector3.up * mouseX);
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}