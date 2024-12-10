using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DualInputController : MonoBehaviour
{
    public Transform playerBody;   // Reference to the player's body (Capsule)
    public Transform playerCamera; // Manually assigned camera
    public CharacterController characterController; // CharacterController for smooth movement
    public float mouseSensitivity = 100f;
    public float moveSpeed = 5f;   // Speed of player movement
    public float vrLookSensitivity = 1f;

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
            HandleVRLook();
            HandleVRMovement();
        }
        else
        {
            HandleMouseLook();
            HandleKeyboardMovement();
        }
    }

    // Handle mouse look
    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -70f, 70f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    // Handle keyboard movement
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

    // Handle VR look
    void HandleVRLook()
    {
        Quaternion headRotation;
        InputDevice headDevice = InputDevices.GetDeviceAtXRNode(XRNode.Head);
        if (headDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out headRotation))
        {
            Vector3 euler = headRotation.eulerAngles * vrLookSensitivity;
            xRotation = Mathf.Clamp(euler.x, -70f, 70f);

            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.localRotation = Quaternion.Euler(0f, euler.y, 0f);
        }
        else
        {
            Debug.LogWarning("Failed to get VR headset rotation.");
        }
    }

    // Handle VR movement
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

