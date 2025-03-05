using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject enemyContainer;

    private float spawnRangeX = 10;
    private float spawnPosY = 7.8f;

    private bool stopSpawning = false;

    private float startDelay = 2;
    private float spawnInterval = 2.5f;

    void Start()
    {
        InvokeRepeating("SpawnRandomEnemy", startDelay, spawnInterval); // coroutine would allow us to stop proccess
    }

    void SpawnRandomEnemy()
    {
        if (stopSpawning)
        {
            return;
        }
        int index = Random.Range(0, enemyPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnPosY, 0);

        GameObject newEnemy = Instantiate(enemyPrefabs[index], spawnPos, enemyPrefabs[index].transform.rotation);
        newEnemy.transform.parent = enemyContainer.transform;
    }

    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }
}
