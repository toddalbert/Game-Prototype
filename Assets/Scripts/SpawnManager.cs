using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _powerUpPrefabs;

    private float _spawnRangeX = 10;
    private float _spawnPosY = 7.8f;
    private bool _stopSpawning = false;
    private float _spawnInterval = 2.5f;

    void Start()
    {
        StartCoroutine(SpawnRandomEnemy());
        StartCoroutine(SpawnPowerUp());
    }

    IEnumerator SpawnRandomEnemy()
    {

        yield return new WaitForSeconds(0.5f);

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

        yield return new WaitForSeconds(2.5f);

        while (!_stopSpawning)
        {
            int index = Random.Range(0,_powerUpPrefabs.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-_spawnRangeX, _spawnRangeX), _spawnPosY, 0);
            Instantiate(_powerUpPrefabs[index], spawnPos, _powerUpPrefabs[index].transform.rotation);
            yield return new WaitForSeconds(Random.Range(5, 11));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
