using UnityEngine;
using System.Collections;

public class Character : Entity {

    public SpriteRenderer SelectionMarker;
    public float speed = .15f;
    //public Attack attack;
    public BoxCollider2D attackCollider;
    public GameObject attackEffect;

    public EventManager eventManager;

    bool playerFocus = false; // Is the player controlling this character right now?
    bool canMove = true;

    Rigidbody2D rigidBody;
    BoxCollider2D collider;

    Vector3 positionBeforeTome = new Vector3(0, 0, 0); //used to restore the character's previous position after putting down the tome.
	// Use this for initialization
	void Awake () 
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<BoxCollider2D>();
        SelectionMarker.enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (playerFocus && !isDead && canMove)
        {
            handleInput();
        }

	}

    void handleInput()
    {
        Vector2 force = new Vector2(0,0);

        /*float x_translation = Input.GetAxis("Horizontal") * speed;
        float y_translation = Input.GetAxis("Vertical") * speed;
        transform.Translate(x_translation, y_translation, 0);*/

        //GetComponent<Rigidbody2D>().velocity = new Vector2(move* maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            force.x = -speed;
            //transform.Translate(-speed, 0, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            force.x = speed;
            //transform.Translate(speed, 0, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            force.y = speed;
            //transform.Translate(0,speed, 0);
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            force.y = -speed;
            //transform.Translate(0,-speed, 0);
        }
        repositionAttack(force);

        rigidBody.AddForce(force);
    }

    //So hack-y, but it wooorks.
    void repositionAttack(Vector2 force)
    {
        if (!attackEffect.activeSelf)
        {
            Vector2 newOffset = new Vector2(0, 0);
            Vector3 newPosition = new Vector3(0, 0, 1);
            bool needsChange = false; //If this isn't here, it'll set it to the 0'd newOffset and newPosition above.

            if (Mathf.Abs(force.x) != 0)
            {
                float plusMinus = (force.x / speed);
                newOffset.x = .75f * plusMinus;
                newPosition.x = 1.1f * plusMinus;
                needsChange = true;
            }

            if (Mathf.Abs(force.y) != 0)
            {
                float plusMinus = (force.y / speed);
                newOffset.y = .75f * plusMinus;
                newPosition.y = 1.1f * plusMinus;
                needsChange = true;
            }

            if (needsChange)
            {
                attackCollider.offset = newOffset;
                attackEffect.transform.localPosition = newPosition;
            }
        }
    }

    public bool isPlayerFocus()
    {
        return playerFocus;
    }

    public void focusOn()
    {
        playerFocus = true;
        SelectionMarker.enabled = true;
    }

    public void focusOff()
    {
        playerFocus = false;
        SelectionMarker.enabled = false;
    }

    public void pickUpTome(Tome t)
    {
        positionBeforeTome = transform.localPosition;
        transform.localPosition = t.transform.position;
        canMove = false;
        //toggleCollider(false);
        sanityRegenActive = false;
    }

    public void putDownTome()
    {
        transform.localPosition = positionBeforeTome;
        canMove = true;
        //toggleCollider(true);
        sanityRegenActive = true;
    }

    public override void death()
    {
        isDead = true;
        toggleCollider(false);
        eventManager.decrementNumOfLivingCharacters();
    }

    void toggleCollider(bool toggle)
    {
        collider.enabled = toggle;
    }
}
