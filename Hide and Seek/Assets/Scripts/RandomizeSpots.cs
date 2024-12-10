using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSpots : MonoBehaviour
{
    public GameObject[] players; // Assign your 7 player GameObjects here
    public Transform[] spawnPoints; // Assign spawn points here

    void Start()
    {
        ScatterPlayers();
    }

    public void ScatterPlayers()
    {
        List<Transform> availablePoints = new List<Transform>(spawnPoints);

        foreach (var player in players)
        {
            int randomIndex = Random.Range(0, availablePoints.Count);
            Transform chosenPoint = availablePoints[randomIndex];

            player.transform.position = chosenPoint.position;
            player.transform.rotation = chosenPoint.rotation;

            availablePoints.RemoveAt(randomIndex); // Prevent reusing the same spawn point
        }
    }
}
