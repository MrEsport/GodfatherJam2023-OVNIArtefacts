using NaughtyAttributes;
using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TextAction : TextElementBase
{
    private enum InputActionType { ButtonColor, SpecificButton }
    private enum SingleButton { R1, G1, B2, R2, G3, B3 }


    [SerializeField, ShowIf(nameof(_showTextColorField)), AllowNesting] protected TextColor textColor;
    public override Color GetTextColor
    {
        get
        {
            return textColor switch
            {
                TextColor.BLUE => Color.blue,
                TextColor.RED => Color.red,
                TextColor.GREEN => Color.green,
                TextColor.YELLOW => Color.yellow,
                TextColor.BLACK => Color.black,
                _ => Color.black
            };
        }
    }

    #region Inspector Properties
    [Header("Input Action")]
    [SerializeField] private InputActionType awaitedActionType;
    [SerializeField, ShowIf(nameof(awaitedActionType), InputActionType.ButtonColor), AllowNesting] private InputButton.BColor actionButtonColor;
    [SerializeField, ShowIf(nameof(awaitedActionType), InputActionType.SpecificButton), AllowNesting] private SingleButton actionButton;

    protected virtual bool _showTextColorField { get => true; }
    #endregion

    #region Action Events
    public event UnityAction OnSuccess { add => OnSuccessAction.AddListener(value); remove => OnSuccessAction.RemoveListener(value); }
    [Header("Events")]
    [SerializeField] private GameEvent OnSuccessAction;
    public event UnityAction OnFailed { add => OnFailedAction.AddListener(value); remove => OnFailedAction.RemoveListener(value); }
    [SerializeField] private GameEvent OnFailedAction;
    #endregion

    public void Success()
    {
        GameLoop.NextText();
        // Gain Score/HP
    }
    public void Failed()
    {
        GameLoop.NextText();
        // lose Score/HP
    }


}
