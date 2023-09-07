using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTE
{
    public float TimeLimit;
    public int ScoreBonus;

    public InputButton ButtonToPress;
    public QTERestriction QTERestr;

    public QTE(float timeLimit, int scoreBonus)
    {
        TimeLimit = timeLimit;
        ScoreBonus = scoreBonus;
    }
}
