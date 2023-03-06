using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthSystem : MonoBehaviour
{
    #region Variables
    public float health;
    public int numOfHearts = 7;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private bool isDead = false;

    [Range(0, 1f)] public float alphaCoefficient = 0.7f;

    [Header("Cheats")]
    public bool godMod = false;
    #endregion

    void Update()
    {
        if (health > numOfHearts)   
            health = numOfHearts;   //хп не превышает количества сердец

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < Mathf.RoundToInt(health) ? fullHeart : emptyHeart; //определяем когда полное, а когда пустое
            hearts[i].enabled = i < numOfHearts;                                     //для изменения максимального количества сердец
            if (health < 1 && !isDead)
            {
                SetDead(true);
                AfterDeath.Death();
            }
        }
    }

    bool CanTakeDamage = true;

    public void TakeDamage(float damage)
    {
        if (godMod)
            damage = 0;
        if (CanTakeDamage)
        {
            CanTakeDamage = false;
            health -= damage;
            StartCoroutine(OnHit());
        }
    }

    private IEnumerator OnHit()
    {
        SpriteRenderer PlayerSprite = GetComponent<SpriteRenderer>();
        PlayerSprite.color = new Color(PlayerSprite.color.r, PlayerSprite.color.g, PlayerSprite.color.b, PlayerSprite.color.a - alphaCoefficient);      
        yield return new WaitForSeconds(0.2f);
        PlayerSprite.color = new Color(PlayerSprite.color.r, PlayerSprite.color.g, PlayerSprite.color.b, PlayerSprite.color.a + alphaCoefficient);
        yield return new WaitForSeconds(0.2f);
        PlayerSprite.color = new Color(PlayerSprite.color.r, PlayerSprite.color.g, PlayerSprite.color.b, PlayerSprite.color.a - alphaCoefficient);
        yield return new WaitForSeconds(0.2f);
        PlayerSprite.color = new Color(PlayerSprite.color.r, PlayerSprite.color.g, PlayerSprite.color.b, PlayerSprite.color.a + alphaCoefficient);
        yield return new WaitForSeconds(0.2f);
        PlayerSprite.color = new Color(PlayerSprite.color.r, PlayerSprite.color.g, PlayerSprite.color.b, PlayerSprite.color.a - alphaCoefficient);
        yield return new WaitForSeconds(0.2f);
        PlayerSprite.color = new Color(PlayerSprite.color.r, PlayerSprite.color.g, PlayerSprite.color.b, PlayerSprite.color.a + alphaCoefficient);
        yield return new WaitForSeconds(0.2f);
        CanTakeDamage = true;
    }

    public void SetDead(bool b)
    {
        isDead = b;
    }
}
