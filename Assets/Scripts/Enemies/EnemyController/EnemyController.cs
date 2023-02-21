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

    [SerializeField]
    private List<GameObject> spawnedEnemies;

    // Start is called before the first frame update
    void Start()
    {
        spawnedEnemies = new List<GameObject>();
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            Vector3 spawnPos = new Vector3(Random.Range(-xBound, xBound), ySpawnPos, Random.Range(-zBound, zBound));
            Debug.Log(spawnPos);
            GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            GameObject spawned = Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);
            spawned.transform.parent = gameObject.transform;
            spawned.GetComponent<Drone>().SetController(GetComponent<EnemyController>());
            spawnedEnemies.Add(spawned);
        }
    }

    public void RemoveEnemy(GameObject toRemove)
    {
        spawnedEnemies.Remove(toRemove);
        Destroy(toRemove);
    }
}
