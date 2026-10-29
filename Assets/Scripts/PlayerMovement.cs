// PlayerMovement.cs
// Maintainer: Qingzhe-Song
// Summary: Converts Move and Look input into relative Rigidbody forces and locks the cursor.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [InspectorName("Movement Force")]
    [Tooltip("Scales movement input before applying relative force to the Rigidbody.")]
    public float speed;
    [InspectorName("Turning Torque")]
    [Tooltip("Scales horizontal look input before applying torque around the local Y axis.")]
    public float rotationSpeed;
    private Vector2 movementValue;
    private float lookValue;
    private Rigidbody rb;
    public void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        // PlayerInput uses the OnMove name to deliver the Move action's value.
        movementValue = value.Get<Vector2>() * speed;
    }

    public void OnLook(InputValue value)
    {
        // Ignore vertical look input because the player turns only around the Y axis.
        lookValue = value.Get<Vector2>().x * rotationSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        rb.AddRelativeForce(movementValue.x * Time.deltaTime, 0, movementValue.y * Time.deltaTime);
        rb.AddRelativeTorque(0, lookValue * Time.deltaTime, 0);

        // if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        // {
        //     transform.Translate(0, 0, speed * Time.deltaTime);
        // }
        // if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        // {
        //     transform.Translate(0, 0, -speed * Time.deltaTime);
        // }
        // if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        // {
        //     transform.Translate(-speed * Time.deltaTime, 0, 0);
        // }
        // if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        // {
        //     transform.Translate(speed * Time.deltaTime, 0, 0);
        // }

        // float mouseX = Input.GetAxis("Mouse X");
        // transform.Rotate(0, mouseX * rotationSpeed * Time.deltaTime, 0);
    }
}
