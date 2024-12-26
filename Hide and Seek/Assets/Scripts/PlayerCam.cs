using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public Transform playerView;


    private void Update()
    {
        PlayerEyes();
    }

    public void PlayerEyes()
    {
        transform.position = playerView.transform.position;
    }
}
