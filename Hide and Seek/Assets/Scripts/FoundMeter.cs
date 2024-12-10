using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoundMeter : MonoBehaviour
{
    public GameObject foundPanel;      // Panel to display "Found"
    public Text foundText;             // Text component to update count
    public Transform playerHand;       // Reference to the player's hand object
    public GameObject gameOverPanel;   // The "Game Over" panel
    public float touchDistance = 1.0f; // Distance considered "touching"
    public int totalCPUs = 7;          // Total number of CPUs (adjustable in Inspector)

    private static int foundCount = 0; // Total CPUs found

    void Start()
    {
        // Ensure the panels are hidden at the start
        foundPanel.SetActive(false);
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
        if(Time.timeScale < 1f)
            Time.timeScale = 1f;
        if (foundCount > 0)
            foundCount = 0;
    }

    void Update()
    {
        // Calculate the distance between the CPU and the player's hand
        float distance = Vector3.Distance(transform.position, playerHand.position);

        // Check if the hand is "touching" the CPU
        if (distance <= touchDistance)
        {
            OnFound();
        }
    }

    private void OnFound()
    {
        // Update the found count
        foundCount++;
        foundText.text = "Found: " + foundCount;

        // Show the "Found" panel
        foundPanel.SetActive(true);

        // Hide it after 1.5 seconds
        Invoke("HideFoundPanel", 1.5f);

        // Disable the CPU (make it disappear)
        gameObject.SetActive(false);

        // Check if all CPUs are found
        if (foundCount == totalCPUs)
        {
            TriggerGameOver();
        }
    }

    private void HideFoundPanel()
    {
        foundPanel.SetActive(false);
    }

    private void TriggerGameOver()
    {
        // Show the "Game Over" panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Stop the game
        Time.timeScale = 0f; // Pause the game
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor for UI interaction
        Cursor.visible = true; // Make the cursor visible
    }

    public void ResumeGame()
    {
        // Resume the game when the menu is closed
        Time.timeScale = 1f; // Resume time
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor back
        Cursor.visible = false; // Hide the cursor
    }
}
