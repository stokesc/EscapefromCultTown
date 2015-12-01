using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CultistMove : MonoBehaviour 
{
    public bool debugMode = false;
    public float moveSpeed;
    public GameObject canvas;

    Directions.Direction moveDirection = Directions.none();//Should be assigned to from the static direction variables found in Directions.cs
    bool moving = false;
    Rigidbody2D rigidBody;

    List<Character> charactersInContact = new List<Character>();

    Vector3 normalScale = new Vector3(1.5f, 1.5f, 1f);
    Quaternion normalRotation;

	void Awake () 
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        //Because quaternions are confusing.
        normalRotation = transform.rotation;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (debugMode)
            testCode();

        if (charactersInContact.Count != 0)
        {
            checkForDeadCharacters();
        }
    }

	void FixedUpdate () 
    {
	    if(moving && !rigidBody.isKinematic)
        {
            move();
        }
	}

    void OnCollisionEnter2D(Collision2D collider)
    {
        //if (debugMode) Debug.Log("Collision enter");
        toggleMovementFreeze(true);
        if (collider.gameObject.tag == "Character")
        {
            charactersInContact.Add(collider.gameObject.GetComponent<Character>());
        }
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        //if (debugMode) Debug.Log("Collision exit");
        toggleMovementFreeze(false);
        if (collider.gameObject.tag == "Character")
        {
            charactersInContact.Remove(collider.gameObject.GetComponent<Character>());
        }
    }
   
    void move()
    {
        Vector2 moveVector = new Vector2(moveSpeed, moveSpeed);
        Vector2 moveDir = Directions.getVector2Direction(moveDirection);

        moveVector.x *= moveDir.x;
        moveVector.y *= moveDir.y;

        rigidBody.AddForce(moveVector);
    }

    //Note the following two movement freezing functions are all mutually exclusive. toggleMovementFreeze incorporates them by calling one if toggled to false;

    void freezeXMovement()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    void freezeYMovement()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

    }

    void toggleMovementFreeze(bool toggle)
    {
        if(toggle)
        {
            rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            if (Directions.isHorizontalDirection(moveDirection))
                freezeYMovement();
            else if (Directions.isVerticalDirection(moveDirection))
                freezeXMovement();
        }
    }

    public void setMoveDirection(Directions.Direction dir)
    {
        moveDirection = dir;
        setFacingDirection(dir);

        //So the cultist can't annoyingly be pushed around in an axis other than the one it's moving on.
        if(Directions.isHorizontalDirection(dir))
        {
            freezeYMovement();
        }

        else if(Directions.isVerticalDirection(dir))
        {
            freezeXMovement();
        }

        moving = true;
    }

    //This only works assuming the cultist starts facing left before calling this function.
    //Contains hack-y code )=
    public void setFacingDirection(Directions.Direction dir)
    {
        switch(dir)
        {
            case Directions.Direction.UP:
                transform.rotation = normalRotation;
                transform.Rotate(new Vector3(0, 0, 90));
                canvas.transform.Rotate(new Vector3(0, 0, 90)); //To rotate it back to where it should be...
                transform.localScale = new Vector3(-1 * normalScale.x, normalScale.y, normalScale.z);
                canvas.transform.localScale = new Vector3(-1 * canvas.transform.localScale.x, canvas.transform.localScale.y, canvas.transform.localScale.z); //To flip it back to how it should be...
                break;
            case Directions.Direction.DOWN:
                transform.rotation = normalRotation;
                transform.Rotate(new Vector3(0, 0, 90));
                canvas.transform.Rotate(new Vector3(0, 0, -90)); //To rotate it back to where it should be...
                transform.localScale = normalScale;
                break;
            case Directions.Direction.LEFT:
                transform.rotation = normalRotation;
                transform.localScale = normalScale;
                break;
            case Directions.Direction.RIGHT:
                transform.rotation = normalRotation;
                transform.localScale = new Vector3(-1 * normalScale.x, normalScale.y, normalScale.z);
                canvas.transform.localScale = new Vector3(-1 * canvas.transform.localScale.x, canvas.transform.localScale.y, canvas.transform.localScale.z); //To flip it back to how it should be...
                break;
            default:
                break;
        }

    }


    public void setMoving(bool new_moving)
    {
        moving = new_moving;
    }

    void testCode()
    {
        if (Input.GetKeyDown(KeyCode.A))
            setMoveDirection(Directions.left());
        else if (Input.GetKeyDown(KeyCode.D))
            setMoveDirection(Directions.right());
        else if (Input.GetKeyDown(KeyCode.W))
            setMoveDirection(Directions.up());
        else if (Input.GetKeyDown(KeyCode.S))
            setMoveDirection(Directions.down());


        moving = true;
    }

    //Checks to see if characters the cultist is in contact with are dead or not. If they are, unfreeze movement, 'cause you can go ahead and walk over them.
    //This feels like it's expensive, but I'm not sure how else to do this, since disabling the dead character's collider doesn't trigger OnCollisionExit.
    void checkForDeadCharacters()
    {
        List<Character> toRemove = new List<Character>();

            foreach(Character character in charactersInContact)
            {
                if(character.getIsDead())
                {
                    toggleMovementFreeze(false);
                    toRemove.Add(character);
                }
            }

            if(toRemove.Count != 0)
            {
                foreach(Character deadChar in toRemove)
                {
                    charactersInContact.Remove(deadChar);
                }
            }
    }
    
}
