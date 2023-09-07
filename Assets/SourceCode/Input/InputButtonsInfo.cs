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

    public InputButton GetInputButtonColor(InputButton.BColor color)
    {
        InputButton button = new InputButton() { ButtonCol = color };
        return button;
    }
}
