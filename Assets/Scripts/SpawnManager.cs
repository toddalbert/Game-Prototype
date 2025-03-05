using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private GameObject _enemyContainer;

    private float _spawnRangeX = 10;
    private float _spawnPosY = 7.8f;

    private bool _stopSpawning = false;

    private float _startDelay = 2;
    private float _spawnInterval = 2.5f;

    void Start()
    {
        InvokeRepeating("SpawnRandomEnemy", _startDelay, _spawnInterval); // coroutine would allow us to stop proccess
    }

    void SpawnRandomEnemy()
    {
        if (_stopSpawning)
        {
            return;
        }
        int index = Random.Range(0,_enemyPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-_spawnRangeX, _spawnRangeX), _spawnPosY, 0);

        GameObject newEnemy = Instantiate(_enemyPrefabs[index], spawnPos, _enemyPrefabs[index].transform.rotation);
        newEnemy.transform.parent = _enemyContainer.transform;
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
