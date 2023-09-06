using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class TextAction_RandomColor : TextAction
{
    public override Color GetTextColor
    {
        get
        {
            return (TextColor)Random.Range(0, 4) switch
            {
                TextColor.BLUE => Color.blue,
                TextColor.RED => Color.red,
                TextColor.GREEN => Color.green,
                TextColor.YELLOW => Color.yellow,
                _ => Color.yellow
            };
        }
    }

    protected override bool _showTextColorField { get => false; }
}
