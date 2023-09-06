using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    private static Dialog _instance;

    [SerializeField, Expandable] private Speech speechTextLibrary;

    private TextAction currentTextAction;
    private int _dialogIndex;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this);
        _instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            ReadText(speechTextLibrary.GetRandomTextAction());
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            ReadText(speechTextLibrary.GetRandomTextActionWithRandomColor());
        }
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
        //_instance?.ReadText();
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
        //ReadText();
    }

    private void ReadText(TextElementBase text)
    {
        var color = text.GetTextColor;
        Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), text.Text));
        _dialogIndex++;
    }

    private void ResetTextIndex()
    {
        _dialogIndex = 0;
    }
}
