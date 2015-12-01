using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tome : MonoBehaviour {

    public bool debugMode = false;
    public EventManager eventManager;
    public ChannelBar channelBar;

    public float channelDuration; //in seconds.
    public float progressInterval; //How many seconds does it take accumulate progress on the duration?
    float percentageOfProgressPerInterval;  // progressInterval / channelDuration = this variable's value. 
                                            //Basically, if I want the channel to take 50 seconds, but only want progress to tick up every 5 seconds, 
                                            //how much is that tick worth? (In this case, 10 percent).
    float currentProgress = 0; //A value between 0 and 1. 0 means there is no progress, 1 means that the tome has been completely channeled. 
    float nextProgressTickTimeStamp = 0;

    public float sanityDamageInterval = 0; //in seconds.
    public float sanityDamagePerInterval = 0;
    float nextSanityDamageTickTimeStamp = 0;

    List<Character> charactersInRange = new List<Character>();

    public BoxCollider2D collider;

    bool isPickedUp = false;
    bool hasBeenChanneled = false;
    Character tomeCarrier = null;

	// Use this for initialization
	void Start () 
    {
        percentageOfProgressPerInterval = progressInterval / channelDuration;
        channelBar.toggleVisible(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
        handleInput(); 
        if(isPickedUp && timePastTimeStamp(nextProgressTickTimeStamp) && !hasBeenChanneled)
        {
            currentProgress += percentageOfProgressPerInterval;
            channelBar.setCurrentProgress(currentProgress);
            if (debugMode) Debug.Log("Channel Progress: " + (currentProgress * 100) + "%");
            nextProgressTickTimeStamp = Time.time + progressInterval;
        }

        if(isPickedUp && timePastTimeStamp(nextSanityDamageTickTimeStamp))
        {
            if (debugMode) Debug.Log("Tome is damaging sanity");
            tomeCarrier.damageSanity(sanityDamagePerInterval);
            nextSanityDamageTickTimeStamp = Time.time + sanityDamageInterval;
        }

        if(currentProgress >= 1 && !hasBeenChanneled)
        {
            hasBeenChanneled = true;
            putDown();
            if (debugMode) Debug.Log("You read me like a book :)! ...Wait.");
            eventManager.winGame(2);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Character")
        {
            Character approachingCharacter = other.GetComponent<Character>();
            if (!charactersInRange.Contains(approachingCharacter))
            {
                if (debugMode) Debug.Log("New character has entered Tome pickup range");
                charactersInRange.Add(approachingCharacter);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Character")
        {
            if (debugMode) Debug.Log("Character has exited Tome pickup range");
            charactersInRange.Remove(other.GetComponent<Character>());
        }
    }

    //It's really weird that the input code for picking up a tome is in the tome and not in the character, but: less than 3 hours left.
    void handleInput()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && !isPickedUp)
        {
            if(charactersInRange.Count != 0)
            {
                Character tomeCharacter = charactersInRange[0];
                if(tomeCharacter.isPlayerFocus() && !tomeCharacter.getIsDead())
                {
                    pickUp(tomeCharacter);
                }
            }
        }

        else if (Input.GetKeyDown(KeyCode.Mouse1) && isPickedUp)
        {
            putDown();
        }
    }

    void pickUp(Character character)
    {
        isPickedUp = true;

        collider.enabled = false;

        character.pickUpTome(this);
        tomeCarrier = character;

        nextProgressTickTimeStamp = Time.time + progressInterval;
        nextSanityDamageTickTimeStamp = Time.time + sanityDamageInterval;

        channelBar.toggleVisible(true);
    }

    void putDown()
    {
        isPickedUp = false;

        collider.enabled = true;

        tomeCarrier.putDownTome();
        tomeCarrier = null;

        channelBar.toggleVisible(false);
    }

    bool timePastTimeStamp(float timestamp)
    {
        return (Time.time > timestamp);
    }
}
