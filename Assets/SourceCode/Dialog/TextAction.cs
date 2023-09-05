using System;
using UnityEngine;
using UnityEngine.Events;

public class TextAction : TextElementBase
{
    [Serializable]
    public class ActionEvent : UnityEvent { }

    public event UnityAction OnSuccess { add => OnSuccessAction.AddListener(value); remove => OnSuccessAction.RemoveListener(value); }
    [SerializeField] private ActionEvent OnSuccessAction;
    public event UnityAction OnFailed { add => OnFailedAction.AddListener(value); remove => OnFailedAction.RemoveListener(value); }
    [SerializeField] private ActionEvent OnFailedAction;

    public void Success() => OnSuccessAction?.Invoke();
    public void Failed() => OnFailedAction?.Invoke();
}
