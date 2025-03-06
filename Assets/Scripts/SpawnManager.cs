using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _powerUpPrefab;

    private float _spawnRangeX = 10;
    private float _spawnPosY = 7.8f;
    private bool _stopSpawning = false;
    private float _spawnInterval = 2.5f;

    void Start()
    {
        // InvokeRepeating("SpawnRandomEnemy", _startDelay, _spawnInterval); // coroutine would allow us to stop proccess
        StartCoroutine(SpawnRandomEnemy());
        StartCoroutine(SpawnPowerUp());
    }

    IEnumerator SpawnRandomEnemy()
    {
        while (!_stopSpawning)
        {       
            int index = Random.Range(0,_enemyPrefabs.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-_spawnRangeX, _spawnRangeX), _spawnPosY, 0);
            GameObject newEnemy = Instantiate(_enemyPrefabs[index], spawnPos, _enemyPrefabs[index].transform.rotation);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    IEnumerator SpawnPowerUp()
    {
        while (!_stopSpawning)
        {
            // every 3-7 seconds spawn in powerup
            Vector3 spawnPos = new Vector3(Random.Range(-_spawnRangeX, _spawnRangeX), _spawnPosY, 0);
            Instantiate(_powerUpPrefab, spawnPos, _powerUpPrefab.transform.rotation);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
