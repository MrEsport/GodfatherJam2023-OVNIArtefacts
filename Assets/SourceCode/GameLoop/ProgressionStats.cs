using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Stats")]
public class ProgressionStats : ScriptableObject
{
    [Header("Number of Rounds")]
    [SerializeField] private int roundsBeforeRandomPhase;
    [SerializeField] private int maxRoundsDifficultyCap;

    [SerializeField, ProgressBar("Difficulty Progression", nameof(maxRoundsDifficultyCap))] private int textActionsPlayed;

    [Header("QTE Timer")]
    [SerializeField] private float easyTimeOnScreenInSeconds = 5f;
    [SerializeField] private float hardTimeOnScreenInSeconds = .8f;
    [SerializeField, CurveRange(0f, 0f, 1f, 1f, (EColor.Green ))] private AnimationCurve timeOnScreenLerpCurve;

    [Header("HP")]
    [SerializeField] private int healthGain;
    public int GetHPGain { get => healthGain; }
    [SerializeField] private int healthLoss;
    public int GetHPLoss { get => healthLoss; }

    [Header("Score")]
    [SerializeField] private int scoreGain;
    [SerializeField] private int scoreLoss;
    public int GetScoreLoss { get => scoreLoss; }
    [SerializeField, CurveRange(0f, 1f, 1f, 10f, EColor.Red)] private AnimationCurve scoreMultiplierProgression;

    private float difficultyProgressionPercentage { get => textActionsPlayed / (float)maxRoundsDifficultyCap; }
    public bool HasStarted { get => textActionsPlayed > 0; }
    public bool IsRoundInSecondPhase { get => textActionsPlayed >= roundsBeforeRandomPhase; }

    public float GetTimeOnScreen { get => Mathf.Lerp(easyTimeOnScreenInSeconds, hardTimeOnScreenInSeconds ,timeOnScreenLerpCurve.Evaluate(difficultyProgressionPercentage)); }
    public int GetScoreGain { get => Mathf.RoundToInt(scoreGain * scoreMultiplierProgression.Evaluate(difficultyProgressionPercentage)); }

    public void Init()
    {
        textActionsPlayed = 0;
    }

    public void IncrementActionsPlayed() => textActionsPlayed++;
}
