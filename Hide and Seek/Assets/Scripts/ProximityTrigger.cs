using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityTrigger : MonoBehaviour
{
    public GameObject closePanel;       // "You're Close!" panel
    public GameObject closerPanel;      // "You're Getting Closer!" panel
    public GameObject tooFarPanel;      // "Too Far!" panel

    public float closeDistance = 2f;    // Distance for "You're Close!"
    public float closerDistance = 5f;   // Distance for "You're Getting Closer!"
    public float tooFarDistance = 8f;   // Distance for "Too Far!"

    public bool isClose = false;        // A boolean to check if the player is close

    private void Start()
    {
        // Ensure panels are initially disabled
        if (closePanel) closePanel.SetActive(false);
        if (closerPanel) closerPanel.SetActive(false);
        if (tooFarPanel) tooFarPanel.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Calculate the distance from the center of the collider to the player
            float distance = Vector3.Distance(transform.position, other.transform.position);

            // Activate the appropriate panel based on the distance
            if (distance <= closeDistance)
            {
                isClose = true; // Set the close value to true if the player is close enough
                if (closePanel) closePanel.SetActive(true);
                if (closerPanel) closerPanel.SetActive(false);
                if (tooFarPanel) tooFarPanel.SetActive(false);
            }
            else
            {
                isClose = false; // Set to false if player is not close
                if (closePanel) closePanel.SetActive(false);
            }

            if (distance <= closerDistance && distance > closeDistance)
            {
                if (closerPanel) closerPanel.SetActive(true);
                if (tooFarPanel) tooFarPanel.SetActive(false);
            }
            else if (distance <= tooFarDistance && distance > closerDistance)
            {
                if (tooFarPanel) tooFarPanel.SetActive(true);
            }
            else
            {
                if (tooFarPanel) tooFarPanel.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Disable all panels when the player exits the trigger area
            if (closePanel) closePanel.SetActive(false);
            if (closerPanel) closerPanel.SetActive(false);
            if (tooFarPanel) tooFarPanel.SetActive(false);
        }
    }
}
