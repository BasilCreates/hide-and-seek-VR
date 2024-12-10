using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    void Update()
    {
        //if the user applies the escape key..
        if (Input.GetKey("escape"))
        {
            //quits the game.
            Application.Quit();
        }
    }
    public void MainGame()
    {
        //Loads our TestScene to see if the game;
        SceneManager.LoadScene("Main Game");
    }
    public void MainMenu()
    {
        //Loads our TestScene to see if the game;
        SceneManager.LoadScene("Hide and Seek - Title Screen");
    }
    public void StartOver()
    {
        //Loads our TestScene to see if the game;
        SceneManager.LoadScene("Main Game");
    }
    public void About()
    {
        //Loads our TestScene to see if the game;
        SceneManager.LoadScene("About");
    }

    public void Rules()
    {
        //Loads our TestScene to see if the game;
        SceneManager.LoadScene("Rules");
    }
}
