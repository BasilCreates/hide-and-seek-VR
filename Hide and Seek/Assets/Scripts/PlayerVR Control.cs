using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVRControl : MonoBehaviour
{
    public Transform playerView;
    public GameObject cameraPosition;


    private void Update()
    {
        PlayerEyes();
    }

    public void PlayerEyes()
    {
        GetComponent<Camera>().transform.position = playerView.transform.position;
    }
}
