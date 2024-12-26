using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class VRController : MonoBehaviour
{
    public float speed = 1.0f;
    private XROrigin rigg;
    public XRNode inputSource;
    private Vector2 inputAxis;
    private CharacterController character;

    void Start()
    {
        character = GetComponent<CharacterController>();
        rigg = GetComponent<XROrigin>();
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    void FixedUpdate()
    {
        // Get the camera's rotation in the Y-axis (yaw), to make the movement direction relative to the camera's facing direction
        Quaternion headYaw = Quaternion.Euler(0, rigg.Camera.transform.eulerAngles.y, 0);

        // Calculate the movement direction based on the camera's rotation and input axis
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        // Move the character in that direction
        character.Move(direction * Time.fixedDeltaTime * speed);

        // Ensure the character is always facing the direction it's moving
        if (direction.magnitude > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        }
    }
}
