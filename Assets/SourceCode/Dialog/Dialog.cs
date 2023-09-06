using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{
    #region Static Instance
    private static Dialog _instance;
    #endregion

    #region Events
    public static event UnityAction<InputButton> OnTextActionRead { add => _instance?.onTextActionRead.AddListener(value); remove => _instance?.onTextActionRead.RemoveListener(value); }
    [SerializeField] private GameEvent<InputButton> onTextActionRead = new GameEvent<InputButton>();
    #endregion

    [SerializeField, Expandable] private Speech speechTextLibrary;

    private TextAction _currentTextAction;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this);
        _instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            ReadText(speechTextLibrary.GetRandomTextAction());
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            ReadText(speechTextLibrary.GetRandomTextActionWithRandomColor());
        }
    }

    #region Static Singleton Functions
    public static void Read(GameplayState currentState)
    {
        _instance?.ReadTextAction(currentState);
    }
    #endregion

    private void ReadTextAction(GameplayState currentState)
    {
        _currentTextAction = currentState == GameplayState.FirstPhase ?
            speechTextLibrary.GetRandomTextAction() :
            speechTextLibrary.GetRandomTextActionWithRandomColor();

        // onTextActionRead?.Invoke( // Get InputButton from TextAction // )
        BindActionEvents();
        ReadText(_currentTextAction);
    }

    private void ReadText(TextElementBase text)
    {
        var color = text.GetTextColor;
        Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), text.Text));
    }

    private void BindActionEvents()
    {
        // QTESystem.OnQTESuccess += _currentTextAction.Success;
        // QTESystem.OnQTEFail += _currentTextAction.Fail;
    }

    private void UnbindActionEvents()
    {
        // QTESystem.OnQTESuccess -= _currentTextAction.Success;
        // QTESystem.OnQTEFail -= _currentTextAction.Fail;
    }
}
