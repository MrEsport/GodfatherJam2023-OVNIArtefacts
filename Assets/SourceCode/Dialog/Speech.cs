using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Dialog Speech")]
public class Speech : ScriptableObject
{
    [SerializeField, ReorderableList] private List<TextAction> predefinedTextInputAction = new List<TextAction>();
    [SerializeField, ReorderableList] private List<TextAction_RandomColor> randomColorTextInputAction = new List<TextAction_RandomColor>();
    [SerializeField, ReorderableList] private List<TextCommentary> feedbackText = new List<TextCommentary>();
    [SerializeField, ReorderableList] private List<TextCommentary> uselessCommentary = new List<TextCommentary>();

    public TextAction GetRandomTextAction()
    {
        return predefinedTextInputAction[Random.Range(0, predefinedTextInputAction.Count)];
    }

    public TextAction GetRandomTextActionWithRandomColor()
    {
        return randomColorTextInputAction[Random.Range(0, randomColorTextInputAction.Count)];
    }

    public TextElementBase GetRandomFeedbackText
        ()
    {
        return feedbackText[Random.Range(0, feedbackText.Count)];
    }

    public TextElementBase GetRandomCommentary()
    {
        return uselessCommentary[Random.Range(0, uselessCommentary.Count)];
    }
}
