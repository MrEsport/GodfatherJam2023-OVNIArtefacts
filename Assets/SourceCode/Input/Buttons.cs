using System;
using UnityEngine;

[Serializable] public struct InputButton
{
    public enum BColor
    {
        BLUE, RED, YELLOW, GREEN
    }

    public enum BLabel
    {
        Blue1, Red1, Yellow1, Blue2, Red2, Green2, Blue3, Yellow3, Green3, Green4, Red4, Yellow4
    }

    public BColor ButtonCol;
    public BLabel ButtonLabel;
}
