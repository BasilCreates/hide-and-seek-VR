using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DualInputController : MonoBehaviour
{
    public Transform playerBody;   // Reference to the player's body (Capsule)
    public Transform playerCamera; // Manually assigned camera
    public CharacterController characterController; // CharacterController for smooth movement
    public float moveSpeed = 5f;   // Speed of player movement
    public float rotationSpeed = 100f;  // Rotation speed for joystick input

    private float xRotation = 0f;  // Tracks vertical rotation (looking up/down)
    private bool isVRActive;

    void Start()
    {
        // Ensure required components are assigned
        if (playerCamera == null || playerBody == null || characterController == null)
        {
            Debug.LogError("Please assign all required components in the Inspector.");
            return;
        }

        // Lock the cursor for non-VR testing
        Cursor.lockState = CursorLockMode.Locked;

        // Detect if a VR headset is connected
        isVRActive = XRSettings.isDeviceActive;
    }

    void Update()
    {
        if (playerCamera == null || playerBody == null || characterController == null) return;

        if (isVRActive)
        {
            HandleVRLook(); // Handle VR head look
            HandleVRMovement();
        }
        else
        {
            HandleKeyboardMovement(); // Non-VR controls (keyboard)
        }
    }

    // Handle keyboard movement for non-VR (testing purposes)
    void HandleKeyboardMovement()
    {
        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down arrow keys

        // Calculate direction relative to the player’s forward direction
        Vector3 move = (playerBody.forward * vertical + playerBody.right * horizontal).normalized;

        // Apply movement using the CharacterController
        characterController.Move(move * moveSpeed * Time.deltaTime);
    }

    // Handle VR head look (headset rotation controls both body and camera)
    void HandleVRLook()
    {
        InputDevice headDevice = InputDevices.GetDeviceAtXRNode(XRNode.Head);
        Quaternion headRotation;

        // Check if the device is valid and get the rotation from the headset
        if (headDevice.isValid && headDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out headRotation))
        {
            // Extract the rotation in Euler angles
            Vector3 eulerAngles = headRotation.eulerAngles;

            // Apply the yaw (horizontal rotation) to the player body (rotate the body)
            float headYaw = eulerAngles.y; // Yaw is the horizontal rotation
            playerBody.rotation = Quaternion.Euler(0f, headYaw, 0f); // Rotate the player body based on yaw

            // Apply the pitch (vertical rotation) to the camera (up/down look)
            xRotation = eulerAngles.x; // Pitch is the vertical rotation
            xRotation = Mathf.Clamp(xRotation, -70f, 70f); // Limit vertical rotation to avoid upside-down view
            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Debugging: Print the head rotation values
            // Debug.Log("Head Rotation: " + eulerAngles);
        }
        else
        {
            Debug.LogWarning("Failed to retrieve VR headset rotation.");
        }
    }

    // Handle VR movement using the left joystick
    void HandleVRMovement()
    {
        InputDevice leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        Vector2 inputVector;

        if (leftHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputVector))
        {
            // Calculate movement direction relative to the player's forward direction
            Vector3 move = (playerBody.forward * inputVector.y + playerBody.right * inputVector.x).normalized;

            // Apply movement using the CharacterController
            characterController.Move(move * moveSpeed * Time.deltaTime);
        }
    }
}
