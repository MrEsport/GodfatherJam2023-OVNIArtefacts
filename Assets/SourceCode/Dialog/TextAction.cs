using NaughtyAttributes;
using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TextAction : TextElementBase
{
    private enum InputActionType { ButtonColor, SpecificButton }


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

    public QTERestriction GetRestriction
    {
        get
        {
            return (awaitedActionType == InputActionType.ButtonColor) ?
                QTERestriction.COLOR :
                QTERestriction.LABEL;
        }
    }

    public InputButton.BColor GetButtonColor { get => actionButtonColor; }
    public InputButton.BLabel GetButtonLabel { get => actionButton; }

    #region Inspector Properties
    [Header("Input Action")]
    [SerializeField] private InputActionType awaitedActionType;
    [SerializeField, ShowIf(nameof(awaitedActionType), InputActionType.ButtonColor), AllowNesting] private InputButton.BColor actionButtonColor;
    [SerializeField, ShowIf(nameof(awaitedActionType), InputActionType.SpecificButton), AllowNesting] private InputButton.BLabel actionButton;

    protected virtual bool _showTextColorField { get => true; }
    #endregion
}
