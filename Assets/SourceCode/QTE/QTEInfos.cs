using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/QTETypeInfos", order = 1)]
public class QTEInfos : ScriptableObject
{
    public int QTEMinorTimeLimit;
    public int QTEMediumTimeLimit;
    public int QTEMajorTimeLimit;

    public int QTEMinorScoreBonus;
    public int QTEMediumScoreBonus;
    public int QTEMajorScoreBonus;

    public QTE CreateQTE(QTE.QTEType type)
    {
        QTE newQTE = new QTE();
        switch (type)
        {
            case (QTE.QTEType.MINOR):
                newQTE.Type = QTE.QTEType.MINOR;
                newQTE.TimeLimit = QTEMinorTimeLimit;
                newQTE.ScoreBonus = QTEMinorScoreBonus;
                break;
            case (QTE.QTEType.MEDIUM):
                newQTE.Type = QTE.QTEType.MEDIUM;
                newQTE.TimeLimit = QTEMediumTimeLimit;
                newQTE.ScoreBonus = QTEMediumScoreBonus;
                break;
            case (QTE.QTEType.MAJOR):
                newQTE.Type = QTE.QTEType.MAJOR;
                newQTE.TimeLimit = QTEMajorTimeLimit;
                newQTE.ScoreBonus = QTEMajorScoreBonus;
                break;
        }
        newQTE.ButtonToPress.ButtonPos = InputButton.BPosition.LEFT;
        newQTE.ButtonToPress.ButtonCol = InputButton.BColor.BLUE;
        newQTE.ButtonToPress.ButtonLab = InputButton.BLabel.N1;

        Debug.Log(
            "You need to press the " + newQTE.ButtonToPress.ButtonPos + " "
            + newQTE.ButtonToPress.ButtonCol + " on the "
            + newQTE.ButtonToPress.ButtonLab + "button "
            );
        Debug.Log("You have " + newQTE.TimeLimit + " seconds ! Bonus : " + newQTE.ScoreBonus);
        return newQTE;
    }


}
