using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTE
{

    public enum QTEType
    {
        MINOR, MEDIUM, MAJOR
    }
    public QTEType Type;
    public float TimeLimit;
    public int ScoreBonus;

    public InputButton ButtonToPress;


}
