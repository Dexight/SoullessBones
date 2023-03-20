using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public GameObject enemyPrefab;
    public int maxEnemies;
    public float spawnDelay;
    public List<Transform> spawnPoints;
    private int currentEnemies = 0;

    [SerializeField] private bool isRoom = false;
    private TimeManager timeManager;
    private void Awake()
    {
        if(SceneStats.stats.Contains("Spawners"))
        {
            Destroy(gameObject);
        }
            
        timeManager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (!timeManager.TimeIsStopped)
            {
                if (currentEnemies < maxEnemies && spawnPoints.Count != 0)
                {
                    int spawnIndex = Random.Range(0, spawnPoints.Count);
                    Transform spawnPoint = spawnPoints[spawnIndex];
                    GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                    currentEnemies++;
                    enemy.GetComponent<Enemy>().spawner = this;
                }
                else
                {
                    if (isRoom)
                    {
                        itemPrefab.SetActive(true);
                        Destroy(gameObject);
                        break;
                    }
                    else
                    {
                        Destroy(gameObject);
                        break;
                    }
                }

                yield return new WaitForSeconds(spawnDelay);
            }
            yield return new WaitForSeconds(0);
        }
    }

    public void EnemyDestroyed()
    {
        currentEnemies--;
    }

    public void SpawnerRemove(Transform transform)
    {
        spawnPoints.Remove(transform);
    }
}
