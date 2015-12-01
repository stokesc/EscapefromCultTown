using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	public void loadAScene(int scene)
    {
        Application.LoadLevel(scene);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
