using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField, Range(0,100)] float HealthBarPercentage;

    [SerializeField] float playerMaxHP;
    [SerializeField] float playerHP;
    [SerializeField] int playerScore;

    [SerializeField] int debugHPAmount;

    #region Unity Methods

    private void OnValidate()
    {
        if(playerHP > playerMaxHP) playerHP = playerMaxHP;
        playerHP = playerMaxHP * HealthBarPercentage;
        UpdateHealthbar();
    }
    private void Start()
    {
        Debug.Log("Player HP: >" +  playerHP);
        Debug.Log("Player Score: >" +  playerScore);
    }
    #endregion

    #region Gain/Lose methods
    void GainHP(int amount)
    {
        playerHP += Mathf.Abs(amount);
        Debug.Log("Player gained " + amount + " HPs.");
        CheckHP();
    }
    void LoseHP(int amount)
    {
        playerHP -= Mathf.Abs(amount);
        Debug.Log("Player lost " + amount + " HPs.");
        CheckHP();
    }
    void GainScore(int amount)
    {
        playerScore += Mathf.Abs(amount);
        Debug.Log("Score : " + playerScore);
    }
    void LoseScore(int amount)
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
        Mathf.Clamp(playerHP, 0, playerMaxHP);
        Mathf.Max(playerMaxHP, 0);
        HealthBarPercentage = playerHP / playerMaxHP * 100;
        Mathf.Clamp(HealthBarPercentage, 0, 100);
        healthBar.rectTransform.localScale = new Vector3(HealthBarPercentage, healthBar.rectTransform.localScale.y);
        Debug.Log("Health : " + HealthBarPercentage);
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
