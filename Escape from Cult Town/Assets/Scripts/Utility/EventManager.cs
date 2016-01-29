using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {
    //Handles victory and failure conditions.
    //Handles pausing
    //Handles character switching

    public bool debugMode = false;

    public List<Character> characters = new List<Character>();
    public CharacterCamera camera;

    int focusedCharacter;
    int defaultFocusedCharacter = 0;

    int numOfLivingCharacters;

    const int CHAR_1 = 0;
    const int CHAR_2 = 1;
    const int CHAR_3 = 2;
	
    void Start()
    {
        numOfLivingCharacters = characters.Count;
        characters[defaultFocusedCharacter].focusOn();
        focusedCharacter = defaultFocusedCharacter;

        camera.setCharacters(characters);
        camera.setFocus(defaultFocusedCharacter);
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
        if(Input.GetKeyDown(KeyCode.Alpha1) && focusedCharacter != CHAR_1)
        {
            switchFocus(CHAR_1);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2) && focusedCharacter != CHAR_2)
        {
            switchFocus(CHAR_2);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3) && focusedCharacter != CHAR_3)
        {
            switchFocus(CHAR_3);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void switchFocus(int newFocus)
    {
        characters[focusedCharacter].focusOff();
        characters[newFocus].focusOn();
        camera.setFocus(newFocus);

        focusedCharacter = newFocus;
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
        SceneManager.LoadScene(SceneList.VICTORY);
    }

    //Should only be called if all Characters are dead. 
    void loseGame()
    {
        if(debugMode) Debug.Log("Ha ha ha you lost.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
