using NaughtyAttributes;
using System.Security.Cryptography;
using UnityEngine;

public class IngameAnimations : MonoBehaviour
{
    [SerializeField] SpriteRenderer yunikaObj;
    [SerializeField] Sprite[] yunikaSprites;
    int currentSprite;

    [SerializeField] GameObject spotlight;
    [SerializeField,Range(0,100)] int fallingChance = 50;
    [SerializeField,Range(0,3)] int debugSprite;

    [SerializeField] GameObject loseScreen;
    [SerializeField] GameObject speechBubbles;


    private void Awake()
    {
        yunikaObj = yunikaObj.gameObject.GetComponent<SpriteRenderer>();    
    }

    private void Start()
    {
        loseScreen.SetActive(false);
        speechBubbles.SetActive(true);
        currentSprite = 2;
        QTESystem.OnQTESuccess += QTESuccess;
        QTESystem.OnQTEFail += QTEfail;
        Player.OnPlayerLose += GameOver;
        ChangeYunikaSprite(2);
    }


    #region Yunika

    void QTESuccess()
    {
        currentSprite++;
        ChangeYunikaSprite(currentSprite);
    }

    void QTEfail()
    {
        currentSprite--;
        ChangeYunikaSprite(currentSprite);
        SpotlightFall();
    }
    void ChangeYunikaSprite(int newSprite)
    {
        newSprite = Mathf.Clamp(newSprite, 1, yunikaSprites.Length - 1);
        currentSprite = Mathf.Clamp(currentSprite, 1, yunikaSprites.Length - 1);
        yunikaObj.sprite = yunikaSprites[newSprite];
    }

    [Button]
    void DebugChangeYunikaSprite()
    {
        ChangeYunikaSprite(debugSprite);
    }

    #endregion

    #region Spotlight Fall

    [Button]
    void SpotlightFall()
    {
        int randChance = Random.Range(1, 100);
        if (randChance >= fallingChance) return;
        float randPos = Random.Range(-8, 8);
        Vector3 pos = new Vector3 (transform.position.x + randPos, transform.position.y, transform.position.z);
        Instantiate(spotlight,pos, Quaternion.Euler(0,0, Random.Range(-180, 180)));
    }

    #endregion

    void GameOver()
    {
        yunikaObj.sprite = yunikaSprites[0];
        loseScreen.gameObject.SetActive(true);
        speechBubbles.gameObject.SetActive(false);
    }

}
