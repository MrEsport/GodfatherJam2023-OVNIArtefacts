using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] List<InputButton> inputButtons;

    #region Game Events
    public GameEvent<InputButton> _OnInputPressed;

    #endregion

    private void Awake()
    {

    }

    public void OnInputButton1Blue(InputAction.CallbackContext context)
    {
        if (context.started) _OnInputPressed.Invoke(inputButtons[0]);
    }
    public void OnInputButton1Red(InputAction.CallbackContext context)
    {
        if (context.started) DebugButton(1);
    }
    public void OnInputButton1Yellow(InputAction.CallbackContext context)
    {
        if (context.started) DebugButton(2);
    }

    public void OnInputButton2Blue(InputAction.CallbackContext context)
    {
        if (context.started) DebugButton(3);
    }

    public void OnInputButton2Red(InputAction.CallbackContext context)
    {
        if (context.started) DebugButton(4);
    }

    public void OnInputButton2Green(InputAction.CallbackContext context)
    {
        if (context.started) DebugButton(5);
    }
    public void OnInputButton3Blue(InputAction.CallbackContext context)
    {
        if (context.started) DebugButton(6);
    }
    public void OnInputButton3Yellow(InputAction.CallbackContext context)
    {
        if (context.started) DebugButton(7);
    }
    public void OnInputButton3Green(InputAction.CallbackContext context)
    {
        if (context.started) DebugButton(8);
    }
        public void OnInputButton4Green(InputAction.CallbackContext context)
    {
        if (context.started) DebugButton(9);
    }
        public void OnInputButton4Red(InputAction.CallbackContext context)
    {
        if (context.started) DebugButton(10);
    }
        public void OnInputButton4Yellow(InputAction.CallbackContext context)
    {
        if (context.started) DebugButton(11);
    }

    void DebugButton(int ButtonIndex)
    {
        Debug.Log(
        "Button " + inputButtons[ButtonIndex].ButtonPos +
        " , Color " + inputButtons[ButtonIndex].ButtonCol +
        " , Label " + inputButtons[ButtonIndex].ButtonLab
    );
    }

}
