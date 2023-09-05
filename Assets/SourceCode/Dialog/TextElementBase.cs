using NaughtyAttributes;
using System;
using UnityEngine;

[Serializable]
public class TextElementBase
{
    public string Line { get => textLine; }
    [SerializeField, ResizableTextArea] private string textLine;
}