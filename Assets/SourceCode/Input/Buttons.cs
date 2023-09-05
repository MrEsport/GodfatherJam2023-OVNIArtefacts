using System;
using UnityEngine;

[Serializable] public struct InputButton
{
    public enum BPosition
    {
        LEFT, TOP, RIGHT, BOTTOM
    }
    public enum BColor
    {
        BLUE, RED, YELLOW, GREEN
    }

    public enum BLabel
    {
        N1, N2, N3, N4
    }

    public BPosition ButtonPos;
    public BColor ButtonCol;
    public BLabel ButtonLab;
}
