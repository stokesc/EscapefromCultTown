using UnityEngine;
using System.Collections;

public class MenuFunctions : MonoBehaviour {

	public void loadAScene(int scene)
    {
        Application.LoadLevel(scene);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
