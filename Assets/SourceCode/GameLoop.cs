using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class GameLoop : MonoBehaviour
{
    #region Static Instance
    private static GameLoop _instance;
    #endregion

    #region Events
    public static event UnityAction OnPlayerLose { add => _instance?.onPlayerLose.AddListener(value); remove => _instance?.onPlayerLose.RemoveListener(value); }
    [SerializeField] private GameEvent onPlayerLose = new GameEvent();
    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this);
        _instance = this;
    }

    private void Start()
    {
        onPlayerLose.AddListener(Defeat);
    }

    #region Static Singleton Functions
    public static void StartGame()
    {

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

    private void Defeat()
    {
        Debug.Log("Player Dead lol, Game Over  x.x");
    }

    private void OnDestroy()
    {
        onPlayerLose?.RemoveAllListeners();
    }
}
