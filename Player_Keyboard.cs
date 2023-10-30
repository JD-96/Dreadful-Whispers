using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ImprovedMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f; // Adjust to your desired movement speed
    public float groundDrag = 6f; // Ground drag to reduce slipperiness
    public Transform orientation;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        myInput();
    }

    private void myInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        // Store the current Y-velocity to maintain it
        float yVelocity = rb.velocity.y;

        // Apply drag on the horizontal plane to reduce slipperiness
        rb.drag = groundDrag;

        // Reassign the Y-velocity to maintain it
        Vector3 newVelocity = moveDirection * moveSpeed;
        newVelocity.y = yVelocity;
        rb.velocity = newVelocity;
    }
}
