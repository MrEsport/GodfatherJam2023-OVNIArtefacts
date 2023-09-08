using NaughtyAttributes;
using UnityEngine;

public class IngameAnimations : MonoBehaviour
{
    [SerializeField] SpriteRenderer yunikaObj;
    [SerializeField] Sprite[] yunikaSprites;
    int currentSprite;

    [SerializeField] GameObject spotlight;
    [SerializeField,Range(0,100)] int fallingChance = 50;
    [SerializeField,Range(0,3)] int debugSprite;


    private void Awake()
    {
        yunikaObj = yunikaObj.gameObject.GetComponent<SpriteRenderer>();    
    }

    private void Start()
    {
        QTESystem.OnQTESuccess += QTESuccess;
        QTESystem.OnQTEFail += QTEfail;
        Player.OnPlayerLose += () => ChangeYunikaSprite(0);
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
        Mathf.Clamp(newSprite, 1, yunikaSprites.Length);
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

}
