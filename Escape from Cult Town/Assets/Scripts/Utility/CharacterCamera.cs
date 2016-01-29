using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Follows the currently focused character. If the focus changes, it travels to the new
//focused character and follows them. 
public class CharacterCamera : MonoBehaviour 
{
    List<Character> characters = new List<Character>();

    int currentFocus = -1;
    Vector3 targetPos;

    bool hitTarget = true;

    void Awake()
    {
        targetPos = transform.position;
    }

    void Update()
    {
        Debug.Log(hitTarget);
        if(currentFocus != -1)
        {
            if(!hitTarget)
            {
                moveToTargetPos();
            }
            else
            {
                transform.position = getCameraPosForChar(currentFocus);
            }

        }
    }

    public void setFocus(int newFocus)
    {
        currentFocus = newFocus;
        targetPos = getCameraPosForChar(newFocus);
        hitTarget = false;
    }

    public void setCharacters(List<Character> chars)
    {
        characters = chars;
    }

    Vector3 getCameraPosForChar(int focus)
    {
        Vector3 charPos = characters[focus].transform.position;
        return new Vector3(charPos.x, charPos.y, -1); // If we copy the character's z, nothing is visible. Set it to in front of all the 2D stuff.
    }

    void moveToTargetPos()
    {
        Vector3 target_distance = new Vector3(targetPos.x - transform.position.x, targetPos.y - transform.position.y, -1);
        float newX = transform.position.x + 0.1f * target_distance.x;
        float newY = transform.position.y + 0.1f * target_distance.y;

        transform.position = new Vector3(newX, newY, -1);

        if (transform.position == targetPos)
        {
            hitTarget = true;
        }
    }

	
}
