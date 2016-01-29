using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CultistSpawner : MonoBehaviour {

    public bool debugMode = false;

    public Directions.Direction spawnDir;
    public GameObject cultistPrefab;

    public bool endlessWaves = false; //This will probably be usually true, as cultists should continue to spawn until the tome is completely channeled. 
    //Basically of these should be able to be adjusted based on what wave it is. Something like a dictionary, with the key as the wave num and the value as the appropriate variable.
    //That'll have to be a future implementation, though. For now, every wave is the same.
    public float spawnInterval = 3; //In seconds
    public float initialWaveDelay = 2; //In seconds
    public float delayBetweenWaves = 5; //In seconds
    public int maxNumOfCultists = 2;
    public int cultistsPerWave = 3; 
    public int numberOfWaves = 2;
    
    float nextSpawnTimeStamp;
    float nextWaveTimeStamp;
    int livingCultists = 0;
    int remainingWaves;
    int remainingCultistsInWave;
    bool waveOngoing = false;
    List<GameObject> spawnedCultists;

	// Use this for initialization
	void Start () 
    {
        orientStairs();
        spawnedCultists = new List<GameObject>();
        nextSpawnTimeStamp = Time.time + initialWaveDelay;
        nextWaveTimeStamp = Time.time + initialWaveDelay;
        remainingWaves = numberOfWaves;
        remainingCultistsInWave = cultistsPerWave;
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (debugMode) Debug.Log("Living Cultists: " + livingCultists);
        /*if (debugMode)
            debugCode();*/

        //If it's time to start a new wave AND there isn't already a wave ongoing AND (There are waves remaining OR the waves are endless)...
        if (timePastTimeStamp(nextWaveTimeStamp) && !waveOngoing && (remainingWaves > 0 || endlessWaves))
        {
            //Start Wave by setting "wave ongoing" to true.
            if (debugMode) Debug.Log("Wave Starting");
            waveOngoing = true;
        }
        //If a wave is ongoing AND there are cultists left in this wave...
        if (waveOngoing && remainingCultistsInWave > 0)
        {
            if (debugMode) Debug.Log("Wave ongoing and remaining cultists in wave. " + remainingCultistsInWave + " cultists remaining.");
            //If it's time to spawn a cultist AND there are less than the maximum number of living cultists...
            if (timePastTimeStamp(nextSpawnTimeStamp) && maxNumOfCultists > livingCultists)
            {
                if (debugMode) Debug.Log("Time to spawn a cultist, and there are less living cultists than the max. Living Cultists: " + livingCultists);
                //Spawn Cultist (this function increments the number of living cultists and calls the cultist's "connect Spawner" method.)
                spawnCultist();
                //Decrement number of cultists left in this wave.
                remainingCultistsInWave--;
                //Set timestamp for the next spawn
                nextSpawnTimeStamp = Time.time + spawnInterval;
            }
        }
        //If there are no more cultists left in this wave AND all of this wave's cultists are dead...
        if (remainingCultistsInWave <= 0 && livingCultists == 0)
        {
            if (debugMode) Debug.Log("No more cultists left in wave, and they're all dead. Stopping this wave, and setting timestamp for the next one.");
            //Decrement number of remaining waves.
            remainingWaves--;
            //Set timestamp for when the next wave should spawn based on the delay between waves.
            nextWaveTimeStamp = Time.time + delayBetweenWaves;
            //Set wave ongoing to false.
            waveOngoing = false;
            //Reset number of "remaining cultists" based on the number of cultists per wave.
            remainingCultistsInWave = cultistsPerWave;
        }

	}

    void spawnCultist()
    {
        GameObject newCultist = (GameObject) Object.Instantiate(cultistPrefab, transform.position, cultistPrefab.transform.rotation);
        newCultist.GetComponent<CultistMove>().setMoveDirection(spawnDir);
        newCultist.GetComponent<Enemy>().connectSpawner(this);

        livingCultists++;
    }

    public void reportDeath()
    {
        livingCultists--;
    }

    //Rotates stairs based on which way the cultists will be going.
    void orientStairs()
    {
        Vector3 stairRotation = new Vector3(0, 0, 0);
        switch(spawnDir)
        {
            case Directions.Direction.DOWN:
                stairRotation.z = 270;
                transform.Rotate(stairRotation);
                break;
            case Directions.Direction.LEFT:
                stairRotation.z = 180;
                transform.Rotate(stairRotation);
                break;
            case Directions.Direction.RIGHT:
                //The stair starts out facing the correct way, but just in case...
                transform.Rotate(stairRotation);
                break;
            case Directions.Direction.UP:
                stairRotation.z = 90;
                transform.Rotate(stairRotation);
                break;
            default:
                break;
        }
    }

    bool timePastTimeStamp(float timestamp)
    {
        return (Time.time > timestamp);
    }

    void debugCode()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            spawnCultist();
    }
}


