using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SanityBar : StatusBar {

    public float sanityRegenPerSecond = 2f;
    float sanityRegenTimeStamp = 0;
    bool hasRegenTicked = false;

	// Use this for initialization
	void Start () 
    {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () 
    {
        base.Update();

        if (entityOwner.isSanityRegenActive())
        {
            if (currentStatus < maxStatus && hasRegenTicked)
            {
                refreshSanityRegen();
            }

            if (Time.time > sanityRegenTimeStamp && !hasRegenTicked)
            {
                modifyCurrentStatus(sanityRegenPerSecond);
                hasRegenTicked = true;
            }
        }

        if (debugMode)
        {   //Debug code, get rid of later!
            if (Input.GetKeyDown(KeyCode.LeftBracket))
                currentStatus -= .05f * maxStatus;
            if (Input.GetKeyDown(KeyCode.RightBracket))
                currentStatus += .05f * maxStatus;
        }
	}

    void refreshSanityRegen()
    {
        sanityRegenTimeStamp = Time.time + 1;
        hasRegenTicked = false;
    }
}
