using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {
    //Handles victory and failure conditions.
    //Handles pausing
    //Handles character switching

    public bool debugMode = false;

    public List<Character> characters = new List<Character>();

    int numOfLivingCharacters;
	
    void Start()
    {
        numOfLivingCharacters = characters.Count;
        characters[0].togglePlayerFocus(true);
    }

    void Update () 
    {
        handleInput();

        //if (debugMode && Input.GetKeyDown(KeyCode.PageUp))
          //  winGame();
        if (debugMode && Input.GetKeyDown(KeyCode.PageDown))
            loseGame();
        if (numOfLivingCharacters == 0)
            loseGame();

	}

    void handleInput()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            characters[0].togglePlayerFocus(true);
            characters[1].togglePlayerFocus(false);
            characters[2].togglePlayerFocus(false);

        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            characters[0].togglePlayerFocus(false);
            characters[1].togglePlayerFocus(true);
            characters[2].togglePlayerFocus(false);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            characters[0].togglePlayerFocus(false);
            characters[1].togglePlayerFocus(false);
            characters[2].togglePlayerFocus(true);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void decrementNumOfLivingCharacters()
    {
        numOfLivingCharacters--;
        if (debugMode) Debug.Log("Living Characters: " + numOfLivingCharacters);
    }

    public void incrementNumOfLivingCharacters()
    {
        numOfLivingCharacters++;
    }

    public void winGame(float victoryDelay)
    {
        Invoke("winGame", victoryDelay); //This is a real dumb way to do this, but, y'know, 45 minutes to go.
    }

    public void winGame()
    {
        if (debugMode) Debug.Log("Victoryyyy");
        Application.LoadLevel(2);
    }

    //Should only be called if all Characters are dead. 
    void loseGame()
    {
        if(debugMode) Debug.Log("Ha ha ha you lost.");
        Application.LoadLevel (Application.loadedLevelName);
    }
}
