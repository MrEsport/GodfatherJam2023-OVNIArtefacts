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

    public QTE CreateQTE(QTE.QTEType type, InputButton.BColor col, InputButton.BLabel lab, QTERestriction QTERestr)
    {
        QTE newQTE = new QTE(QTE.QTEType.MINOR, QTEMinorTimeLimit, QTEMinorScoreBonus);
        switch (type)
        {
            case (QTE.QTEType.MINOR):
                newQTE = new QTE(QTE.QTEType.MINOR, QTEMinorTimeLimit, QTEMinorScoreBonus);
                break;
            case (QTE.QTEType.MEDIUM):
                newQTE = new QTE(QTE.QTEType.MEDIUM, QTEMediumTimeLimit, QTEMediumScoreBonus);
                break;
            case (QTE.QTEType.MAJOR):
                newQTE = new QTE(QTE.QTEType.MAJOR, QTEMajorTimeLimit, QTEMajorScoreBonus);
                break;
        }

        newQTE.ButtonToPress.ButtonCol = col;
        newQTE.ButtonToPress.ButtonLabel = lab;
        newQTE.QTERestr = QTERestr;

        return newQTE;
    }


}