using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public enum GameplayState
{
    Starting,
    FirstPhase,
    SecondPhase,
    Ending
}

public class GameLoop : MonoBehaviour
{
    #region Static Instance
    private static GameLoop _instance;
    #endregion

    #region Events
    public static event UnityAction OnPlayerLose { add => _instance?.onPlayerLose.AddListener(value); remove => _instance?.onPlayerLose.RemoveListener(value); }
    [SerializeField] private GameEvent onPlayerLose = new GameEvent();
    #endregion

    [SerializeField] private int firstPhaseActionsToPlay = 4;

    private GameplayState _currentGameState;
    private int _textActionPlayed;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this);
        _instance = this;
    }

    private void Start()
    {
        onPlayerLose.AddListener(Defeat);

        _currentGameState = GameplayState.FirstPhase;
    }

    private void Update()
    {
        if (_textActionPlayed <= 0 && _currentGameState == GameplayState.FirstPhase)
            StartDialogAction();
    }

    #region Static Singleton Functions
    public static void StartGame()
    {

    }

    public static void NextText()
    {
        _instance?.CheckUpdatePhase();
        _instance?.StartDialogAction();
    }

    /// <summary>
    /// Exit current Game, Go back to Game Select
    /// </summary>
    public static void ExitGame()
    {
        _instance?.Defeat();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        return;
#endif
        Application.Quit();
    } 
    #endregion

    private void CheckUpdatePhase()
    {
        if (_textActionPlayed >= firstPhaseActionsToPlay)
            _currentGameState = GameplayState.SecondPhase;
    }

    private void StartDialogAction()
    {
        Dialog.GenerateTextAction(_currentGameState);
        _textActionPlayed++;
    }

    private void Defeat()
    {
        Debug.Log("Player Dead lol, Game Over  x.x");
        _currentGameState = GameplayState.Ending;
    }

    private void OnDestroy()
    {
        onPlayerLose?.RemoveAllListeners();
    }
}
