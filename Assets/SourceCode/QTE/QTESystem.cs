using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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
    public static event UnityAction<float> OnQTETick { add => _instance?.onQTETick?.AddListener(value); remove => _instance?.onQTETick?.RemoveListener(value); }
    [SerializeField] private GameEvent<float> onQTETick = new GameEvent<float>();
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

        Player.OnPlayerLose += StopQTE;

        OnQTESuccess += () => Player._instance.GainHP(currentQTE.HPBonus);
        OnQTESuccess += () => Player._instance.GainScore(currentQTE.ScoreBonus);

        OnQTEFail += () => Player._instance.LoseHP(currentQTE.HPMalus);
        OnQTESuccess += () => Player._instance.LoseScore(currentQTE.ScoreMalus);
    }

    void NewQTE(InputButton button, QTERestriction restr)
    {
        currentQTE = QTEObj.CreateQTE(button.ButtonCol, button.ButtonLabel, restr);
        DebugDisplayQTE(currentQTE);
        currentQTECoroutine = StartCoroutine(QTETimer(currentQTE.TimeLimit));
    }

    IEnumerator QTETimer(float timeInSeconds)
    {
        float timer = timeInSeconds;
        while (timer > 0)
        {
            onQTETick?.Invoke(timer / timeInSeconds);
            yield return new WaitForSeconds(Time.deltaTime);
            timer -= Time.deltaTime;
        }

        if (currentQTE != null)
            FailedQTE();

        GameLoop.NextAction();
    }

    void InputReceived(InputButton input)
    {
        if (currentQTE == null) return;

        if (VerifyQTEInput(currentQTE, currentQTECoroutine, input))
            SucceededQTE();
        else
            FailedQTE();
    }
    
    bool VerifyQTEInput(QTE QTEToVerify, Coroutine QTETimer, InputButton InputToVerify)
    {
        if (currentQTE == null || currentQTECoroutine == null)
            return false;

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
            case QTERestriction.COLOR:
                Debug.Log("You need to press any" + QTEToDisplay.ButtonToPress.ButtonCol + " button ");
                break;
        }
        Debug.Log("You have " + QTEToDisplay.TimeLimit + " seconds ! Bonus : " + QTEToDisplay.ScoreBonus);
    }

    void SucceededQTE()
    {
        if (currentQTE == null)
            return;

        onQTESuccess?.Invoke();
        currentQTE = null;
    }

    void FailedQTE()
    {
        if (currentQTE == null)
            return;

        onQTEFail?.Invoke();
        currentQTE = null;
    }

    void StopQTE()
    {
        if(currentQTECoroutine != null)
            StopCoroutine(currentQTECoroutine);
        currentQTECoroutine = null;
        currentQTE = null;
    }

    private void OnDestroy()
    {
        onQTESuccess?.RemoveAllListeners();
        onQTEFail?.RemoveAllListeners();
        onQTETick?.RemoveAllListeners();
    }
}