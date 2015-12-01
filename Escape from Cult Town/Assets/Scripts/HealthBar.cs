using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : StatusBar {

	// Use this for initialization
	void Start () 
    {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () 
    {
        base.Update();

	    //Debug code, get rid of later!
        if (debugMode)
        {
            if (Input.GetKeyDown(KeyCode.Semicolon))
                currentStatus -= .05f * maxStatus;
            if (Input.GetKeyDown(KeyCode.Quote))
                currentStatus += .05f * maxStatus;
        }
    }
}
