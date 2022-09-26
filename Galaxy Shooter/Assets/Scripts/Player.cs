using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;

    void Start()
    {
        // Start position
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn manager is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    public void PlayerMovement()
    {
        //input from user
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //movement(left, right, up, down)
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        //restraints
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    public void FireLaser()
    {
        Vector3 laserOffset = new Vector3(transform.position.x, transform.position.y + 1.05f, 0);
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, laserOffset, Quaternion.identity);
    }

    public void Damage()
    {
        _lives--;

        if (_lives < 1)
        {
            _spawnManager.StopSpawning();
            Destroy(this.gameObject);
        }
    }
}
