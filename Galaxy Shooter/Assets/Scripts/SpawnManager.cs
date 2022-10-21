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
    private GameObject[] _powerups;

    private bool _isSpawning = true;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);

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
        yield return new WaitForSeconds(3.0f);

        while (_isSpawning == true)
        {
            float randomTime = Random.Range(3, 8);
            float randomX = Random.Range(-9.0f, 9.0f);
            Vector3 spawnPosition = new Vector3(randomX, 7.5f, 0);

            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerup], spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(randomTime);
        }
        
    }

    public void StopSpawning()
    {
        _isSpawning = false;
    }
}
