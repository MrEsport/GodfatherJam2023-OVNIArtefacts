using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTE
{
    public float TimeLimit;
    public int ScoreBonus;
    public int ScoreMalus;
    public int HPBonus;
    public int HPMalus;

    public InputButton ButtonToPress;
    public QTERestriction QTERestr;

    public QTE(float timeLimit, int scoreBonus, int scoreMalus, int hpBonus, int hpMalus)
    {
        TimeLimit = timeLimit;
        ScoreBonus = scoreBonus;
        ScoreMalus = scoreMalus;
        HPBonus = hpBonus;
        HPMalus = hpMalus;
    }
}
