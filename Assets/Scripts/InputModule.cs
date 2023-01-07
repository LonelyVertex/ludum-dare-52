using UnityEngine;

public class InputModule : MonoBehaviour
{
    [SerializeField] KeyCode _actionButtonKeyCode;
    [SerializeField] KeyCode _leftKeyCode;
    [SerializeField] KeyCode _leftKeyCodeAlt;
    [SerializeField] KeyCode _rightKeyCode;
    [SerializeField] KeyCode _rightKeyCodeAlt;
    [SerializeField] KeyCode _upKeyCode;
    [SerializeField] KeyCode _upKeyCodeAlt;
    [SerializeField] KeyCode _downKeyCode;
    [SerializeField] KeyCode _downKeyCodeAlt;

    public float GetHorizontalAxis()
    {
        float result = 0;
        if (Input.GetKey(_leftKeyCode) || Input.GetKey(_leftKeyCodeAlt))
        {
            result += 1;
        }

        if (Input.GetKey(_rightKeyCode) || Input.GetKey(_rightKeyCodeAlt))
        {
            result -= 1;
        }

        return result;
    }

    public float GetVerticalAxis()
    {
        float result = 0;
        if (Input.GetKey(_downKeyCode) || Input.GetKey(_downKeyCodeAlt))
        {
            result -= 1;
        }

        if (Input.GetKey(_upKeyCode) || Input.GetKey(_upKeyCodeAlt))
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