using UnityEngine;
using System.Collections;

public class Directions : MonoBehaviour {

    public enum Direction { UP, DOWN, LEFT, RIGHT, NONE }

    public static Vector3 NONE = new Vector3(0, 0, 0);
    public static Vector3 UP = new Vector3(0, 2.56f, 0);
    public static Vector3 DOWN = new Vector3(0, -2.56f, 0);
    public static Vector3 LEFT = new Vector3(-2.56f, 0, 0);
    public static Vector3 RIGHT = new Vector3(2.56f, 0, 0);

    static public Direction none()
    {
        return Direction.NONE;
    }

    static public Direction up()
    {
        return Direction.UP;
    }

    static public Direction down()
    {
        return Direction.DOWN;
    }

    static public Direction left()
    {
        return Direction.LEFT;
    }

    static public Direction right()
    {
        return Direction.RIGHT;
    }

    static public bool isADirection(Vector3 dir)
    {
        return (dir == UP || dir == DOWN || dir == LEFT || dir == RIGHT);
    }

    static public bool isHorizontalDirection(Vector3 dir)
    {
        return (dir == LEFT || dir == RIGHT);
    }

    static public bool isHorizontalDirection(Direction dir)
    {
        return (dir == Direction.LEFT || dir == Direction.RIGHT);
    }

    static public bool isVerticalDirection(Vector3 dir)
    {
        return (dir == UP || dir == DOWN);
    }

    static public bool isVerticalDirection(Direction dir)
    {
        return (dir == Direction.UP || dir == Direction.DOWN);
    }

    static public Vector3 getOppositeDirection(Vector3 dir)
    {
        if (dir == UP)
        {
            return DOWN;
        }

        else if(dir == DOWN)
        {
            return UP;
        }

        else if (dir == LEFT)
        {
            return RIGHT;
        }

        else if (dir == RIGHT)
        {
            return LEFT;
        }

        else
        {
            return NONE;
        }
    }

    static public Vector2 getVector2Direction(Vector3 dir)
    {
        if (dir == UP)
        {
            return Vector2.up;
        }

        else if (dir == DOWN)
        {
            return -Vector2.up;
        }

        else if (dir == LEFT)
        {
            return -Vector2.right;
        }

        else if (dir == RIGHT)
        {
            return Vector2.right;
        }

        else
        {
            return Vector2.zero;
        }
    }

    static public Vector2 getVector2Direction(Direction dir)
    {

        switch (dir)
        {
            case Direction.UP:
                return Vector2.up;

            case Direction.DOWN:
                return -Vector2.up;

            case Direction.LEFT:
                return -Vector2.right;

            case Direction.RIGHT:
                return Vector2.right;

            default:
                return Vector2.zero;
        }
    }

    static public Vector3 getRandomDirection()
    {
        int ran = Random.Range(0, 4);

        switch(ran)
        {
            case 0:
                return UP;
            case 1:
                return DOWN;
            case 2:
                return LEFT;
            case 3:
                return RIGHT;
            default:
                return NONE;
        }
    }
}
