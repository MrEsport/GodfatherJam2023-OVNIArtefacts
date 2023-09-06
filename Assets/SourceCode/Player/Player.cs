using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int playerHP;
    [SerializeField] int playerScore;

    [SerializeField] int debugHPAmount;

    #region Unity Methods
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
    }
    #endregion

    #region Debug methods
    [Button] void DebugGainHP()
    {
        GainHP(debugHPAmount);
    }

    [Button] void DebugLoseHP()
    {
        LoseHP(debugHPAmount);
    }

    #endregion
     
}
