using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField, Range(0,1)] float HealthBarPercentage;

    [SerializeField] float playerMaxHP;
    [SerializeField] float playerHP;
    [SerializeField] int playerScore;

    [SerializeField] int debugHPAmount;

    #region Instance

    public static Player _instance;

    #endregion

    #region Unity Methods

    private void OnValidate()
    {
        playerHP = playerMaxHP * HealthBarPercentage;
        UpdateHealthbar();
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this);
        _instance = this;
    }

    private void Start()
    {
        healthBar.gameObject.SetActive(true);
        Debug.Log("Player HP: >" +  playerHP);
        Debug.Log("Player Score: >" +  playerScore);
    }
    #endregion

    #region Gain/Lose methods
    public void GainHP(int amount)
    {
        playerHP += Mathf.Abs(amount);
        Debug.Log("Player gained " + amount + " HPs.");
        CheckHP();
    }
    public void LoseHP(int amount)
    {
        playerHP -= Mathf.Abs(amount);
        Debug.Log("Player lost " + amount + " HPs.");
        CheckHP();
    }
    public void GainScore(int amount)
    {
        playerScore += Mathf.Abs(amount);
        Debug.Log("Score : " + playerScore);
    }
    public void LoseScore(int amount)
    {
        playerScore -= Mathf.Abs(amount);
        Debug.Log("Score : " + playerScore);
    }
    #endregion

    #region Check Methods

    void CheckHP()
    {
        if (!gameObject.activeSelf) return;

        Debug.Log("HP: " + playerHP);
        if (playerHP <= 0)
        {
            Debug.Log("Died");
            gameObject.SetActive(false);
        }
        else Debug.Log("Alive and doing fine :)");
        UpdateHealthbar();
    }
    #endregion

    #region UI Update

    void UpdateHealthbar()
    {
        if (playerHP > playerMaxHP) playerHP = playerMaxHP;
        Mathf.Clamp(playerHP, 0, playerMaxHP);
        Mathf.Max(0, playerMaxHP);
        HealthBarPercentage = playerHP / playerMaxHP;
        Mathf.Clamp(HealthBarPercentage, 0, 1);
        healthBar.rectTransform.localScale = new Vector3(HealthBarPercentage, healthBar.rectTransform.localScale.y);
    }
    #endregion

    #region Debug methods
    [Button] void DebugGainHP()
    {
        GainHP(debugHPAmount);
        UpdateHealthbar();
    }

    [Button] void DebugLoseHP()
    {
        LoseHP(debugHPAmount);
        UpdateHealthbar();
    }

    #endregion

}
