using NaughtyAttributes;
using System;
using UnityEngine;

[Serializable]
public enum TextColor
{
    BLACK = -1,

    BLUE = 0,
    RED = 1,
    GREEN = 2,
    YELLOW = 3
}

[Serializable]
public abstract class TextElementBase
{
    public string Text { get => text; }
    [SerializeField, ResizableTextArea] private string text;

    public abstract Color GetTextColor { get; }
}