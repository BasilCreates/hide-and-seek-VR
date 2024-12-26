using UnityEngine;
using UnityEngine.XR;

public class HeadsetLook : MonoBehaviour
{
    public Transform xrCamera;  // Reference to your XR camera (the one tracking the headset)

    private InputDevice headset;

    void Start()
    {
        // Try to get the XR headset device at the start
        headset = InputDevices.GetDeviceAtXRNode(XRNode.Head);
    }

    void Update()
    {
        // Ensure the headset is valid
        if (headset.isValid)
        {
            Quaternion headRotation;
            if (headset.TryGetFeatureValue(CommonUsages.deviceRotation, out headRotation))
            {
                // Apply the headset's rotation to the camera's rotation
                xrCamera.localRotation = headRotation;
            }
        }
    }
}
