using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Static Instance
    private static InputManager _instance;
    #endregion

    [SerializeField] List<InputButton> inputButtons;

    #region Game Events
    public static event UnityAction<InputButton> OnInputPressed { add => _instance?.onInputPressed?.AddListener(value); remove => _instance?.onInputPressed?.RemoveListener(value); }
    [SerializeField] private GameEvent<InputButton> onInputPressed;
    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this);
        _instance = this;
    }

    private void Start()
    {
        onInputPressed?.AddListener(DebugButton);
    }

    public void OnInputButton1Blue(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(inputButtons[0]);
    }
    public void OnInputButton1Red(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(inputButtons[1]);
    }
    public void OnInputButton1Yellow(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(inputButtons[2]);
    }
    public void OnInputButton2Blue(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(inputButtons[3]);
    }
    public void OnInputButton2Red(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(inputButtons[4]);
    }
    public void OnInputButton2Green(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(inputButtons[5]);
    }
    public void OnInputButton3Blue(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(inputButtons[6]);
    }
    public void OnInputButton3Yellow(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(inputButtons[7]);
    }
    public void OnInputButton3Green(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(inputButtons[8]);
    }
    public void OnInputButton4Green(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(inputButtons[9]);
    }
    public void OnInputButton4Red(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(inputButtons[10]);
    }
    public void OnInputButton4Yellow(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(inputButtons[11]);
    }

    void DebugButton(InputButton button)
    {
        Debug.Log(
            "Button " + button.ButtonPos +
            " , Color " + button.ButtonCol +
            " , Label " + button.ButtonLab);
    }

    private void OnDestroy()
    {
        onInputPressed?.RemoveAllListeners();
    }
}
