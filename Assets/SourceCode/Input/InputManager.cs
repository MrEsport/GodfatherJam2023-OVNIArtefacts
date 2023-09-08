using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Static Instance
    private static InputManager _instance;
    #endregion

    [SerializeField] InputButtonsInfo InputObj;

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

    public void OnInputButton1Blue(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(InputObj.GetInputButton(0));
    }
    public void OnInputButton1Red(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(InputObj.GetInputButton(1));
    }
    public void OnInputButton1Yellow(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(InputObj.GetInputButton(2));
    }
    public void OnInputButton2Blue(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(InputObj.GetInputButton(3));
    }
    public void OnInputButton2Red(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(InputObj.GetInputButton(4));
    }
    public void OnInputButton2Green(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(InputObj.GetInputButton(5));
    }
    public void OnInputButton3Blue(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(InputObj.GetInputButton(6));
    }
    public void OnInputButton3Yellow(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(InputObj.GetInputButton(7));
    }
    public void OnInputButton3Green(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(InputObj.GetInputButton(8));
    }
    public void OnInputButton4Green(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(InputObj.GetInputButton(9));
    }
    public void OnInputButton4Red(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(InputObj.GetInputButton(10));
    }
    public void OnInputButton4Yellow(InputAction.CallbackContext context)
    {
        if (context.started) onInputPressed?.Invoke(InputObj.GetInputButton(11));
    }

    private void OnDestroy()
    {
        onInputPressed?.RemoveAllListeners();
    }
}
