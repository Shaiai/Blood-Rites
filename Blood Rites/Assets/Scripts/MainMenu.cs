using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{   

    //Function that allows the player to begin the game from clicking the play button on main menu.
    public void PlayGame()
    {
        //Uses scene manager to load current scene based on current scenes index and then adding one to it. 
        SceneManager.LoadScene("Intro");

    }

    //Function to quit game by clicking quit key from main menu.
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ControlMenu()
    {
        SceneManager.LoadScene("Controls");
    }

}
