using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 3.0f;
    [SerializeField]
    private GameObject _laser;
    private SpawnManager _spawnManager;

    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    { 
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_laser, transform.position, Quaternion.identity);
            Destroy(other.gameObject, 3.0f);
            Destroy(this.gameObject, 0.25f);
            _spawnManager.StartSpawning();
        }
    }
}
