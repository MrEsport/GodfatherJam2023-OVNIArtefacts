using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Unity.VisualScripting;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.Events;
using Unity.Mathematics;

#region QTE Misc
public enum QTERestriction
{
    NONE, POSITION, COLOR
}
#endregion

public class QTESystem : MonoBehaviour
{
    [SerializeField] QTEInfos QTEObj;
    [SerializeField] InputButtonsInfo ButtonsObj;

    [SerializeField] QTE.QTEType debugType;
    [Button]
    void DebugCreateQTE()
    {
        QTERestriction debugRestr = (QTERestriction)UnityEngine.Random.Range(0, 3);
        Debug.Log("Restriction : " + debugRestr + ", " + (int)debugRestr);
        NewQTE(debugType, ButtonsObj.GetRandomInputButton(), debugRestr);
    }



    #region Instance
    private static QTESystem _instance;

    #endregion

    #region GameEvents
    public static event UnityAction OnQTESuccess { add => _instance?.onQTESuccess?.AddListener(value); remove => _instance?.onQTESuccess?.RemoveListener(value); }
    [SerializeField] private GameEvent onQTESuccess = new GameEvent();
    public static event UnityAction OnQTEFail { add => _instance?.onQTEFail?.AddListener(value); remove => _instance?.onQTEFail?.RemoveListener(value); }
    [SerializeField] private GameEvent onQTEFail = new GameEvent();

    #endregion

    #region QTE
    QTE currentQTE = null;
    Coroutine currentQTECoroutine = null;
    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this);
        _instance = this;
    }
    private void Start()
    {
        InputManager.OnInputPressed += InputReceived;
    }

    void NewQTE(QTE.QTEType type, InputButton button, QTERestriction restr)
    {
        currentQTE = QTEObj.CreateQTE(type, button.ButtonPos, button.ButtonCol, button.ButtonLab, restr);
        DebugDisplayQTE(currentQTE);
        currentQTECoroutine = StartCoroutine(QTETimer(currentQTE.TimeLimit));
    }

    IEnumerator QTETimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        Debug.Log("<b><color=red> </color></b> TIME OUT, FAILED QTE");
        onQTEFail?.Invoke();
    }


    void InputReceived(InputButton input)
    {
        if (currentQTE == null) return;
        if (VerifyQTEInput(currentQTE, currentQTECoroutine, input))
        {
            onQTESuccess?.Invoke();
            Debug.Log("<b><color=green> </color></b> SUCCEEDED QTE");

        }
        else onQTEFail?.Invoke();
    }
    bool VerifyQTEInput(QTE QTEToVerify, Coroutine QTETimer, InputButton InputToVerify)
    {
        StopCoroutine(QTETimer);
        bool doPositionsMatch = InputToVerify.ButtonPos == QTEToVerify.ButtonToPress.ButtonPos;
        bool doColorsMatch = InputToVerify.ButtonCol == QTEToVerify.ButtonToPress.ButtonCol;
        bool doLabelsMatch = InputToVerify.ButtonLab == QTEToVerify.ButtonToPress.ButtonLab;
        switch (QTEToVerify.QTERestr)
        {
            case QTERestriction.NONE:
                return (doPositionsMatch && doColorsMatch && doLabelsMatch);
            case QTERestriction.POSITION:
                return doPositionsMatch;
            case QTERestriction.COLOR:
                return doColorsMatch;
        }
        Debug.Log("Unknow Restriction");
        return false;
    }

    void DebugDisplayQTE(QTE QTEToDisplay)
    {
        switch (QTEToDisplay.QTERestr)
        {
            case QTERestriction.NONE:
                Debug.Log("You need to press the " + QTEToDisplay.ButtonToPress.ButtonPos + " " + QTEToDisplay.ButtonToPress.ButtonCol + " on the " + QTEToDisplay.ButtonToPress.ButtonLab + "button ");
                break;
            case QTERestriction.POSITION:
                Debug.Log("You need to press any" + QTEToDisplay.ButtonToPress.ButtonPos + " button ");

                break;
            case QTERestriction.COLOR:
                Debug.Log("You need to press any" + QTEToDisplay.ButtonToPress.ButtonCol + " button ");
                break;

        }
        Debug.Log("You have " + QTEToDisplay.TimeLimit + " seconds ! Bonus : " + QTEToDisplay.ScoreBonus);
    }
}