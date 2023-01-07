using UnityEngine;

public class InputModule : MonoBehaviour
{
    [SerializeField] KeyCode _actionButtonKeyCode;
    [SerializeField] KeyCode _leftKeyCode;
    [SerializeField] KeyCode _rightKeyCode;
    [SerializeField] KeyCode _upKeyCode;
    [SerializeField] KeyCode _downKeyCode;

    public float GetHorizontalAxis()
    {
        float result = 0;
        if (Input.GetKey(_leftKeyCode))
        {
            result += 1;
        }
        if (Input.GetKey(_rightKeyCode))
        {
            result -= 1;
        }
        return result;
    }

    public float GetVerticalAxis()
    {
        float result = 0;
        if (Input.GetKey(_downKeyCode))
        {
            result -= 1;
        }
        if (Input.GetKey(_upKeyCode))
        {
            result += 1;
        }
        return result;
    }

    public bool IsActionButtonDown()
    {
        return Input.GetKey(_actionButtonKeyCode);
    }

}
