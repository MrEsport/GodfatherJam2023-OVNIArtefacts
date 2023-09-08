using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/QTETypeInfos", order = 1)]
public class QTEInfos : ScriptableObject
{
    [SerializeField, Expandable] ProgressionStats stats;

    public int QTEMinorTimeLimit;
    public int QTEMediumTimeLimit;
    public int QTEMajorTimeLimit;

    public int QTEMinorScoreBonus;
    public int QTEMediumScoreBonus;
    public int QTEMajorScoreBonus;

    public QTE CreateQTE(InputButton.BColor col, InputButton.BLabel lab, QTERestriction QTERestr)
    {
        QTE newQTE = new QTE(stats.GetTimeOnScreen, stats.GetScoreGain, stats.GetScoreLoss, stats.GetHPGain, stats.GetHPLoss);

        newQTE.ButtonToPress.ButtonCol = col;
        newQTE.ButtonToPress.ButtonLabel = lab;
        newQTE.QTERestr = QTERestr;

        return newQTE;
    }
}