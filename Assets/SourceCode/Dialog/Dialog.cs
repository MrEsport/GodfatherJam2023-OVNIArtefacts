using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    private static Dialog _instance;

    [ReorderableList, SerializeReference] public List<TextElementBase> DialogList = new List<TextElementBase>();

    private int _dialogIndex;

    #region Inspector Functions
    [Button(nameof(AddCommentary))]
    public void AddCommentary()
        => DialogList.Add(new TextCommentary());

    [Button(nameof(AddAction))]
    public void AddAction()
        => DialogList.Add(new TextAction());
    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this);
        _instance = this;
    }

    private void Start()
    {
        IEnumerator DialogCoroutine()
        {
            Begin();
            while (_dialogIndex < DialogList.Count)
            {
                yield return new WaitForSeconds(2);
                Next();
            }
            Stop();
        }

        StartCoroutine(DialogCoroutine());
    }

    /// <summary>
    /// Reads the first Dialog Text from the list
    /// </summary>
    public static void Begin()
    {
        _instance?.ReadFromTheStart();
    }

    /// <summary>
    /// Reads the Next Dialog Text
    /// </summary>
    public static void Next()
    {
        _instance?.ReadText();
    }

    /// <summary>
    /// Resets the Dialog
    /// </summary>
    public static void Stop()
    {
        _instance?.ResetTextIndex();
    }

    private void ReadFromTheStart()
    {
        _dialogIndex = 0;
        ReadText();
    }

    private void ReadText()
    {
        Debug.Log(DialogList[_dialogIndex].Line);
        _dialogIndex++;
    }

    private void ResetTextIndex()
    {
        _dialogIndex = 0;
    }
}
