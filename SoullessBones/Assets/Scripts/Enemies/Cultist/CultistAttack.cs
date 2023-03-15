using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistAttack : MonoBehaviour
{
    public GameObject magicCircle;
    private GameObject portal = null;
    private SpriteRenderer magicSprite;
    [SerializeField][Range(0, 1)] private float timeOfEnterAndExit;
    private CultistMove moving;

    private bool isAttack;
    private bool CanAttack = true;
    private bool HideCircle = false;
    private bool blockSpawn = false;

    [SerializeField] private GameObject portalPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float randomPortalPositionX;
    [SerializeField] private List<GameObject> enemies;
    public Transform leftDotPosition;
    public Transform rightDotPosition;
    public int coolDown;
    //private int countOfEnemiesLast = 2;
    private int countOfEnemies = 0;
    Vector2 whereSpawn;

    private void Start()
    {
        magicSprite = magicCircle.GetComponent<SpriteRenderer>();
        moving = GetComponent<CultistMove>();
    }

    void Update()
    {
        isAttack = moving.isAttack;

        if (isAttack && CanAttack)
        {
            StartCoroutine(spriteEnterCoroutine(magicSprite));
            CanAttack = false;
        }

        if (!isAttack)
        {
            HideCircle = true;
            CanAttack = true;
        }

        if (HideCircle)
        {
            magicSprite.color = new Color(magicSprite.color.r, magicSprite.color.g, magicSprite.color.b, 0);
            HideCircle = false;
        }

        #region Enemies count
        if (countOfEnemies != 0)
            for (int i = 0; i < enemies.Count; i++)
                if (enemies[i] == null) enemies.Remove(enemies[i]);

        countOfEnemies = enemies.Count;

        //if(countOfEnemies == countOfEnemiesLast - 1) // если была уничтожена призывная сущность
        //{
        //    countOfEnemies += 1;
        //    //StartCoroutine(SpawnWithCooldown);
        //}

        if (countOfEnemies < 2 && isAttack && !blockSpawn)
        {
            Spawn();
        }

        //countOfEnemiesLast = countOfEnemies;
        #endregion
    }

    private float GetRandomPos(Transform left, Transform right) => Random.Range(left.position.x, right.position.x);

    private void Spawn()
    {
        blockSpawn = true;
        randomPortalPositionX = GetRandomPos(leftDotPosition, rightDotPosition);
        whereSpawn = new Vector2(randomPortalPositionX, leftDotPosition.position.y);
        //спавн портала (потом сам удаляется)
        SpawnPortal(whereSpawn, ref portal);
        //спавн врага
        SpawnEnemy(whereSpawn);
        //портал удаляется своим скриптом
        StartCoroutine(afterDeletePortal());
    }

    private IEnumerator SpawnWithCooldown()
    {
        yield return new WaitForSeconds(1);
        Spawn();
    }

    #region Spawn Portal
    private void SpawnPortal(Vector2 vector,  ref GameObject portal)
    {
        StartCoroutine(SpawnPortalCoroutine(vector));
    }

    private IEnumerator SpawnPortalCoroutine(Vector2 vector)
    {
        portal = Instantiate(portalPrefab, vector, transform.rotation);
        SpriteRenderer portalSprite = portal.GetComponent<SpriteRenderer>();
        portalSprite.color = new Color(portalSprite.color.r, portalSprite.color.g, portalSprite.color.b, 0f);
        yield return new WaitForSeconds(0.1f);
        portalSprite.color = new Color(portalSprite.color.r, portalSprite.color.g, portalSprite.color.b, portalSprite.color.a + 0.33f);
        yield return new WaitForSeconds(0.1f);
        portalSprite.color = new Color(portalSprite.color.r, portalSprite.color.g, portalSprite.color.b, portalSprite.color.a + 0.33f);
        yield return new WaitForSeconds(0.1f);
        portalSprite.color = new Color(portalSprite.color.r, portalSprite.color.g, portalSprite.color.b, portalSprite.color.a + 0.34f);
    }
    private IEnumerator afterDeletePortal()
    {
        yield return new WaitForSeconds(1f);
        blockSpawn = false;
    }
    #endregion

    #region Spawn Enemy
    private void SpawnEnemy(Vector2 vector)
    {
        StartCoroutine(SpawnEnemyCoroutin(vector));
    }

    private IEnumerator SpawnEnemyCoroutin(Vector2 vector)
    {
        vector = new Vector2(vector.x, vector.y - 1f);
        GameObject enemy = Instantiate(enemyPrefab, vector, transform.rotation);
        enemies.Add(enemy);
        SpriteRenderer enemySprite = enemy.GetComponent<SpriteRenderer>();
        enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
        yield return new WaitForSeconds(0.2f);
    }
    #endregion

    private IEnumerator spriteEnterCoroutine(SpriteRenderer sprite)
    {
        yield return new WaitForSeconds(timeOfEnterAndExit);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + 0.2f);
        yield return new WaitForSeconds(timeOfEnterAndExit);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + 0.2f);
        yield return new WaitForSeconds(timeOfEnterAndExit);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + 0.2f);
        yield return new WaitForSeconds(timeOfEnterAndExit);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + 0.2f);
        yield return new WaitForSeconds(timeOfEnterAndExit);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + 0.2f);
        yield return new WaitForSeconds(timeOfEnterAndExit);
    }
}
