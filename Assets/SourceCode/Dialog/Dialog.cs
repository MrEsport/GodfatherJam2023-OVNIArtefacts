using NaughtyAttributes;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class Dialog : MonoBehaviour
{
    #region Static Instance
    private static Dialog _instance;
    #endregion

    #region Events
    public static event UnityAction<InputButton, QTERestriction> OnTextActionRead { add => _instance?.onTextActionRead.AddListener(value); remove => _instance?.onTextActionRead.RemoveListener(value); }
    [SerializeField] private GameEvent<InputButton, QTERestriction> onTextActionRead = new GameEvent<InputButton, QTERestriction>();
    #endregion

    [SerializeField, Expandable] private Speech speechTextLibrary;
    [SerializeField] private InputButtonsInfo inputButtonsLibrary;

    [Header("UI")]
    [SerializeField] private GameObject idolBubbleObject;
    [SerializeField] private List<GameObject> managerBubbleObjects;
    [SerializeField] private TMP_Text feedback_UIText;
    [SerializeField] private List<Sprite> bubbleSprites;
    private TMP_Text _idoltextAction_UIText;
    private List<TMP_Text> _managertextAction_UITexts;
    private Image _idolBubbleImage;
    private List<Image> _managerBubbleImages;
    private Image _usedBubbleImage;

    private TextAction _currentTextAction;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this);
        _instance = this;
    }

    private void Start()
    {
        Player.OnPlayerLose += StopDialog;

        QTESystem.OnQTEFail += ReadFeedback;
        QTESystem.OnQTEFail += () => SetBubbleSprite(_usedBubbleImage, 2);
        QTESystem.OnQTESuccess += () => SetBubbleSprite(_usedBubbleImage, 1);

        _idoltextAction_UIText = idolBubbleObject.GetComponentInChildren<TMP_Text>();
        _idolBubbleImage = idolBubbleObject.GetComponent<Image>();
        _managertextAction_UITexts = new List<TMP_Text>();
        _managerBubbleImages = new List<Image>();
        for (int i = 0; i < managerBubbleObjects.Count; i++)
        {
            _managertextAction_UITexts.Add(managerBubbleObjects[i].GetComponentInChildren<TMP_Text>());
            _managerBubbleImages.Add(managerBubbleObjects[i].GetComponent<Image>());
        }

        HideAllBubbles();
    }

    #region Static Singleton Functions
    public static void GenerateTextAction(GameplayState currentState)
    {
        _instance?.ReadTextAction(currentState);
    }
    #endregion

    private void ReadTextAction(GameplayState currentState)
    {
        HideAllBubbles();

        _currentTextAction = currentState == GameplayState.FirstPhase ?
            speechTextLibrary.GetRandomTextAction() :
            speechTextLibrary.GetRandomTextActionWithRandomColor();

        QTERestriction restriction = _currentTextAction.GetRestriction;
        InputButton awaitedButton = restriction switch
        {
            QTERestriction.LABEL => inputButtonsLibrary.GetInputButton((int)_currentTextAction.GetButtonLabel),
            QTERestriction.COLOR => inputButtonsLibrary.GetInputButtonColor(_currentTextAction.GetButtonColor),
            _ => throw new Exception("Undefined Text Action Restriction")
        };

        onTextActionRead?.Invoke(awaitedButton, restriction);
        UpdateActionTextUI(_currentTextAction);
    }

    private void ReadFeedback()
    {
        UpdateFeedbackTextUI(speechTextLibrary.GetRandomFeedbackText());
    }

    private void UpdateActionTextUI(TextElementBase text)
    {
        switch (text.Speaker)
        {
            case Speaker.IDOL:
                _usedBubbleImage = _idolBubbleImage;
                UpdateTextUI(_idoltextAction_UIText, text);
                break;
            case Speaker.MANAGER:
                int side = Random.Range(0, 2);
                _usedBubbleImage = _managerBubbleImages[side];
                UpdateTextUI(_managertextAction_UITexts[side], text);
                break;
            default:
                break;
        }
        SetBubbleSprite(_usedBubbleImage, 0);
    }

    private void UpdateTextUI(TMP_Text uiText, TextElementBase text)
    {
        uiText.transform.parent.gameObject.SetActive(true);
        uiText.color = text.GetTextColor;
        uiText.text = text.Text;
    }

    private void UpdateFeedbackTextUI(TextElementBase text)
    {
        feedback_UIText.color = text.GetTextColor;
        feedback_UIText.text = text.Text;
    }

    private void SetBubbleSprite(Image bubble, int index)
    {
        bubble.sprite = bubbleSprites[index];
    }

    private void HideAllBubbles()
    {
        idolBubbleObject.gameObject.SetActive(false);
        for (int i = 0; i < managerBubbleObjects.Count; i++)
            managerBubbleObjects[i].gameObject.SetActive(false);
    }

    private void StopDialog()
    {
        HideAllBubbles();
    }

    private void OnDestroy()
    {
        onTextActionRead.RemoveAllListeners();
    }
}
