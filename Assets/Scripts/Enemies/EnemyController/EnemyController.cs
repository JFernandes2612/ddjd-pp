using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float spawnRate = 3f;
    [SerializeField]
    private float xBound = 15f;
    [SerializeField]
    private float ySpawnPos = 0f;
    [SerializeField]
    private float zBound = 15f;
    [SerializeField]
    private GameObject[] enemyPrefabs;

    private List<GameObject> spawnedEnemies;

    private Coroutine spawningCoroutine;

    private int points;

    private int wave = 0;
    // Initializes list of spawned enemies and starts the spawning process
    void Start()
    {
        spawnedEnemies = new List<GameObject>();
    }

    public void StartSpawning() {
        wave++;
        spawningCoroutine = StartCoroutine(Spawner());
    }

    public void StopSpawning() {
        if (spawningCoroutine != null)
            StopCoroutine(spawningCoroutine);
    }

    public bool stillHasEnemies() {
        return spawnedEnemies.Count > 0;
    }

    public int getPoints() {
        return points;
    }

    private Vector3 CalculateSpawnPos(){
        float xPos, zPos;
        bool inCamX;
        bool inCamY;
        do {
            xPos = Random.Range(-xBound, xBound);
            zPos = Random.Range(-zBound, zBound);
            Vector3 viewPortPoint = Camera.main.WorldToViewportPoint(new Vector3(xPos, ySpawnPos, zPos));
            inCamX = viewPortPoint.x < 1.0 && viewPortPoint.x > 0;
            inCamY = viewPortPoint.y < 1.0 && viewPortPoint.y > 0;
        } while(inCamX && inCamY);

        return new Vector3(xPos, ySpawnPos, zPos);
    }

    // Periodically spawns an enemy
    IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            GameObject spawned = Instantiate(enemyToSpawn, CalculateSpawnPos(), Quaternion.identity);
            spawned.transform.parent = gameObject.transform;
            spawned.GetComponent<Enemy>().SetEnemyController(GetComponent<EnemyController>());
            spawnedEnemies.Add(spawned);
        }
    }

    // Removes an enemy from the game
    public void RemoveEnemy(GameObject toRemove)
    {
        points += toRemove.GetComponent<Enemy>().getPoints();
        spawnedEnemies.Remove(toRemove);
        Destroy(toRemove);
    }
}
