using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _trippleShotPowerupPrefab;

    private bool _isSpawning = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_isSpawning == true)
        {
            float randomX = Random.Range(-9.0f, 9.0f);
            Vector3 spawnPosition = new Vector3(randomX, 7.5f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (_isSpawning == true)
        {
            float randomTime = Random.Range(3, 8);
            float randomX = Random.Range(-9.0f, 9.0f);
            Vector3 spawnPosition = new Vector3(randomX, 7.5f, 0);
            Instantiate(_trippleShotPowerupPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(randomTime);
        }
        
    }

    public void StopSpawning()
    {
        _isSpawning = false;
    }
}
