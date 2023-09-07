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
    LABEL, COLOR
}
#endregion

public class QTESystem : MonoBehaviour
{
    [SerializeField] QTEInfos QTEObj;
    [SerializeField] InputButtonsInfo ButtonsObj;

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
        Dialog.OnTextActionRead += NewQTE;
    }

    void NewQTE(InputButton button, QTERestriction restr)
    {
        currentQTE = QTEObj.CreateQTE(button.ButtonCol, button.ButtonLabel, restr);
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
        bool doColorsMatch = InputToVerify.ButtonCol == QTEToVerify.ButtonToPress.ButtonCol;
        bool doLabelsMatch = InputToVerify.ButtonLabel == QTEToVerify.ButtonToPress.ButtonLabel;
        switch (QTEToVerify.QTERestr)
        {
            case QTERestriction.LABEL:
                return (doColorsMatch && doLabelsMatch);
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
            case QTERestriction.LABEL:
                Debug.Log("You need to press the " + QTEToDisplay.ButtonToPress.ButtonLabel + "button ");
                break;
                break;
            case QTERestriction.COLOR:
                Debug.Log("You need to press any" + QTEToDisplay.ButtonToPress.ButtonCol + " button ");
                break;

        }
        Debug.Log("You have " + QTEToDisplay.TimeLimit + " seconds ! Bonus : " + QTEToDisplay.ScoreBonus);
    }
}