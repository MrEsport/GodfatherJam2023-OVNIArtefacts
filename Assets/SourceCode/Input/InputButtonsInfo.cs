using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputButtonsInfo", menuName = "ScriptableObjects/InputButtonsInfo", order = 3)]
public class InputButtonsInfo : ScriptableObject
{

    [SerializeField] List<InputButton> inputButtons;

    public InputButton GetInputButton(int index)
    {
        return inputButtons[index];
    }

    public InputButton GetRandomInputButton()
    {
        return inputButtons[Random.Range(0,inputButtons.Count)];
    }

    public InputButton GetInputButtonColor(InputButton.BColor color)
    {
        InputButton button = GetRandomInputButton();
        button.ButtonCol = color;
        return button;
    }
}
