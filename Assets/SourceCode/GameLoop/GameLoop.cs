using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

    #region Game Events
    public static event UnityAction OnGameStart { add => _instance?.onGameStart?.AddListener(value); remove => _instance?.onGameStart?.RemoveListener(value); }
    [SerializeField] private GameEvent onGameStart;
    #endregion

    [SerializeField, Expandable] private ProgressionStats stats;

    private GameplayState _currentGameState;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this);
        _instance = this;
    }

    private void Start()
    {
        Player.OnPlayerLose += Defeat;

        _currentGameState = GameplayState.Starting;
    }

    private void Update()
    {
        switch (_currentGameState)
        {
            case GameplayState.Starting:
                StartActionPhase();
                break;

            case GameplayState.FirstPhase:
                if (!stats.HasStarted) StartDialogAction();
                return;

            case GameplayState.SecondPhase:
                return;

            case GameplayState.Ending:
                break;

            default:
                break;
        }
    }

    #region Static Singleton Functions
    public static void StartGame()
    {
    }

    public static void NextAction()
    {
        _instance?.CheckUpdatePhase();
        _instance?.StartDialogAction();
    }

    /// <summary>
    /// Exit current Game, Go back to Game Select
    /// </summary>
    public static void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        return;
#endif
        Application.Quit();
    } 
    #endregion

    private void StartActionPhase()
    {
        _instance?.stats.Init();
        _currentGameState = GameplayState.FirstPhase;
    }

    private void CheckUpdatePhase()
    {
        if (stats.IsRoundInSecondPhase)
            _currentGameState = GameplayState.SecondPhase;
    }

    private void StartDialogAction()
    {
        if (_currentGameState != GameplayState.FirstPhase && _currentGameState != GameplayState.SecondPhase)
            return;

        Dialog.GenerateTextAction(_currentGameState);
        stats.IncrementActionsPlayed();
    }

    private void Defeat()
    {
        Debug.Log("Player Dead lol, Game Over  x.x");
        _currentGameState = GameplayState.Ending;
        InputManager.OnInputPressed += _ => RestartLevel();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        onGameStart?.RemoveAllListeners();
    }
}
