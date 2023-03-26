using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistAttack : MonoBehaviour
{
    public GameObject magicCircle;
    private GameObject portal = null;
    private SpriteRenderer magicSprite;
    [SerializeField][Range(0, 1)] private float timeOfEnterAndExit;
    [SerializeField][Range(0, 1)] private float timeOfEnterAndExitPortal;
    private CultistMove moving;

    private bool isAttack;
    private bool CanAttack = true;
    private bool HideCircle = false;
    private bool blockSpawn = false;

    [SerializeField] private GameObject portalPrefab;
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private float randomPortalPositionX;
    [SerializeField] private List<GameObject> enemies;
    public Transform leftDotPosition;
    public Transform rightDotPosition;

    private int countOfEnemies = 0;
    private int curCount = 0;
    public int maxCountOfEnemies = 3;
    [SerializeField] private float coolDown;
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

        if ((countOfEnemies < maxCountOfEnemies || countOfEnemies < curCount) && isAttack && !blockSpawn)
        {
            if (countOfEnemies < curCount)
            {
                blockSpawn = true;
                Invoke("Spawn", coolDown);
            }
            else
            {
                curCount++;
                Spawn();
            }
        }
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
        //портал удаляется своим скриптом
        StartCoroutine(afterDeletePortal());
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
        yield return new WaitForSeconds(timeOfEnterAndExitPortal);
        portalSprite.color = new Color(portalSprite.color.r, portalSprite.color.g, portalSprite.color.b, portalSprite.color.a + 0.4f);
        yield return new WaitForSeconds(timeOfEnterAndExitPortal);
        portalSprite.color = new Color(portalSprite.color.r, portalSprite.color.g, portalSprite.color.b, portalSprite.color.a + 0.2f);
        yield return new WaitForSeconds(timeOfEnterAndExitPortal);
        portalSprite.color = new Color(portalSprite.color.r, portalSprite.color.g, portalSprite.color.b, portalSprite.color.a + 0.2f);
        yield return new WaitForSeconds(timeOfEnterAndExitPortal);
        portalSprite.color = new Color(portalSprite.color.r, portalSprite.color.g, portalSprite.color.b, portalSprite.color.a + 0.1f);
        yield return new WaitForSeconds(timeOfEnterAndExitPortal);
        portalSprite.color = new Color(portalSprite.color.r, portalSprite.color.g, portalSprite.color.b, portalSprite.color.a + 0.1f);
        yield return new WaitForSeconds(timeOfEnterAndExitPortal);
        //спавн врага
        SpawnEnemy(vector);
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
        vector = new Vector2(vector.x, vector.y);
        int index = Random.Range(0, enemyPrefabs.Count);
        GameObject oneEnemyPrefab = enemyPrefabs[index];
        GameObject enemy = Instantiate(oneEnemyPrefab, vector, transform.rotation);
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
