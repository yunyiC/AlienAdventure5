using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputM
{
    public static float MouseScrollwheel
    {
        get
        {
            return Input.GetAxis("Mouse ScrollWheel");
        }
    }

    public static float MouseMoveX
    {
        get
        {
            return Input.GetAxis("Mouse X");
        }
    }

    public static float MouseMoveY
    {
        get
        {
            return Input.GetAxis("Mouse Y");
        }
    }

    public static float MoveX
    {
        get
        {
            return Input.GetAxis("Horizontal");
        }
    }

    public static float MoveY
    {
        get
        {
            return Input.GetAxis("Vertical");
        }
    }

    public static bool MouseL
    {
        get
        {
            return Input.GetMouseButton(0);
        }
    }

    public static bool MouseR
    {
        get
        {
            return Input.GetMouseButton(1);
        }
    }

    public static bool MouseDownL
    {
        get
        {
            return Input.GetMouseButtonDown(0);
        }
    }

    public static bool MouseDownR
    {
        get
        {
            return Input.GetMouseButtonDown(1);
        }
    }

    public static Vector2 Move
    {
        get
        {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }

    public static Vector2 MouseMove
    {
        get
        {
            return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
    }
}
