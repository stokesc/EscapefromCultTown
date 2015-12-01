using UnityEngine;
using System.Collections;

public class CharacterAttack : Attack {
    Character character;

    void Awake()
    {
        character = gameObject.GetComponent<Character>();
    }

	// Use this for initialization
	void Start () 
    {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () 
    {
        base.Update();

        if (canAttack && !character.getIsDead() && character.isPlayerFocus())
        {
            handleInput();
        }
	}

    void handleInput()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            attackAllTargetsInRange(true);
        }
    }
}
