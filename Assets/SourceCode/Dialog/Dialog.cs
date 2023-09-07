using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{
    #region Static Instance
    private static Dialog _instance;
    #endregion

    #region Events
    public static event UnityAction<InputButton, QTERestriction> OnTextActionRead { add => _instance?.onTextActionRead.AddListener(value); remove => _instance?.onTextActionRead.RemoveListener(value); }
    [SerializeField] private GameEvent<InputButton, QTERestriction> onTextActionRead = new GameEvent<InputButton, QTERestriction>();
    #endregion

    [SerializeField, Expandable] private Speech speechTextLibrary;
    [SerializeField] private InputButtonsInfo inputButtonsLibrary;

    private TextAction _currentTextAction;

    private void Start()
    {
        Player.OnPlayerLose += StopDialog;
        QTESystem.OnQTESuccess += GameLoop.NextText;
        QTESystem.OnQTEFail += GameLoop.NextText;
    }

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
    public static void GenerateTextAction(GameplayState currentState)
    {
        _instance?.ReadTextAction(currentState);
    }
    #endregion

    private void ReadTextAction(GameplayState currentState)
    {
        _currentTextAction = currentState == GameplayState.FirstPhase ?
            speechTextLibrary.GetRandomTextAction() :
            speechTextLibrary.GetRandomTextActionWithRandomColor();

        QTERestriction restriction = _currentTextAction.GetRestriction;
        InputButton awaitedButton = restriction switch
        {
            QTERestriction.LABEL => inputButtonsLibrary.GetInputButton((int)_currentTextAction.GetButtonLabel),
            QTERestriction.COLOR => inputButtonsLibrary.GetInputButtonColor(_currentTextAction.GetButtonColor),
            _ => throw new Exception("Undefined Text Action Restriction")
        };

        onTextActionRead?.Invoke(awaitedButton, restriction);

        ReadText(_currentTextAction);
    }

    private void ReadText(TextElementBase text)
    {
        var color = text.GetTextColor;
        Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), text.Text));
    }

    private void StopDialog()
    {

    }

    private void OnDestroy()
    {
        onTextActionRead.RemoveAllListeners();
    }
}
