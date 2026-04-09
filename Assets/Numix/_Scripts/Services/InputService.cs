using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputService : IInputService
{
    public bool GetTap()
    {
        return Input.GetMouseButtonDown(0);
    }
    public bool GetForward()
    {
        return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
    }
    public bool GetBackward()
    {
        return Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
    }
    public bool GetLeft()
    {
        return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
    }

    public bool GetRight()
    {
        return Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
    }

}
