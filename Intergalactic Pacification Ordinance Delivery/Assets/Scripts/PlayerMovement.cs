using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    enum Orbit { horiOrbit, vertOrbit };

    [SerializeField] Orbit orbit;
    [SerializeField] float camMoveSpeed = 1f;
    public bool invertX, invertY;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float moveX = Time.deltaTime * (Input.GetAxis("Horizontal") * camMoveSpeed);
        float moveY = Time.deltaTime * (Input.GetAxis("Vertical") * camMoveSpeed);

        switch (orbit)
        {
            case Orbit.horiOrbit:
                if (!invertX)
                    transform.Rotate(0, -moveX, 0);
                else if (invertX)
                    transform.Rotate(0, moveX, 0);
                break;
            case Orbit.vertOrbit:
                if (!invertY)
                    transform.Rotate(-moveY, 0, 0);
                else if (invertY)
                    transform.Rotate(moveY, 0, 0);
                break;
            default:
                break;
        }

        // Roll
        //if (Input.GetKey(KeyCode.Q) && !invertRoll)
        //    transform.Rotate(0, 0, Time.deltaTime * camRollSpeed);
        //else if (Input.GetKey(KeyCode.E) && !invertRoll)
        //    transform.Rotate(0, 0, Time.deltaTime * -camRollSpeed);
        //else if (Input.GetKey(KeyCode.Q) && invertRoll)
        //    transform.Rotate(0, 0, Time.deltaTime * -camRollSpeed);
        //else if (Input.GetKey(KeyCode.E) && invertRoll)
        //    transform.Rotate(0, 0, Time.deltaTime * camRollSpeed);
    }
}