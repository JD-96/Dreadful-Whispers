using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Include the UI namespace

public class DoorOpenerWithUI : MonoBehaviour
{
    public float interactionDistance = 2f;
    public KeyCode interactKey = KeyCode.E;
    public Transform door;
    public Transform doorcenter;

    private bool isOpen = false;
    private bool isPlayerNearDoor = false; // Added a variable to track player proximity
    public Text interactionText; // Reference to the UI text element

    private void Update()
    {
        // Check if the player is near the door
        isPlayerNearDoor = IsPlayerNearDoor();

        // Enable or disable the interaction text based on proximity
        if (interactionText != null)
        {
            interactionText.enabled = isPlayerNearDoor;
        }

        if (Input.GetKeyDown(interactKey) && isPlayerNearDoor)
        {
            if (!isOpen)
            {
                OpenDoor();
            }
            else
            {
                CloseDoor();
            }
        }
    }

    private bool IsPlayerNearDoor()
    {
        float distance = Vector3.Distance(transform.position, doorcenter.position);
        return distance <= interactionDistance;
    }

    private void OpenDoor()
    {
        door.Rotate(Vector3.up, 90f);
        isOpen = true;
    }

    private void CloseDoor()
    {
        door.Rotate(Vector3.up, -90f);
        isOpen = false;
    }
}
