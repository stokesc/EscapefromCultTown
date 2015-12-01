using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class MoveTile : MonoBehaviour
{

    public bool locked;
    public bool moveUp, moveDown, moveLeft, moveRight;

    // http://blog.brendanvance.com/2014/04/08/elegant-editor-only-script-execution-in-unity3d/comment-page-1/ 
    void Update()
    {
        if (moveUp && !locked)
        {
            transform.position += Directions.UP;
            moveUp = false;
        }

        if (moveDown && !locked)
        {
            transform.position += Directions.DOWN;
            moveDown = false;
        }

        if (moveLeft && !locked)
        {
            transform.position += Directions.LEFT;
            moveLeft = false;
        }

        if (moveRight && !locked)
        {
            transform.position += Directions.RIGHT;
            moveRight = false;
        }
    }

}
