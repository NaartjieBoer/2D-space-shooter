using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.5f;
    private float _speedMultiplier = 2.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _TripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private bool _isTrippleShot = false;
    private bool _isShieldOn = false;

    [SerializeField]
    private GameObject _shieldObject;

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

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);
        
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
        _canFire = Time.time + _fireRate;

        if (_isTrippleShot == true)
        {
            Instantiate(_TripleShotPrefab, transform.position, Quaternion.identity);
        }
        else {
            Vector3 laserOffset = new Vector3(transform.position.x, transform.position.y + 1.05f, 0);
            Instantiate(_laserPrefab, laserOffset, Quaternion.identity);
        }
    }

    public void Damage()
    {
        if (_isShieldOn == true)
        {
            _isShieldOn = false;
            _shieldObject.SetActive(false);
            return;
        }

        _lives--;

        if (_lives < 1)
        {
            _spawnManager.StopSpawning();
            Destroy(this.gameObject);
        }
    }

    public void ActivateTrippleShot()
    {
        _isTrippleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        float waitTIme = 5.0f;
        while(_isTrippleShot == true)
        {
            yield return new WaitForSeconds(waitTIme);
            _isTrippleShot = false;
        }
    }

    public void ActivateSpeedBoost()
    {
        StartCoroutine(SpeedBoostPowerDownRoutime());
    }

    IEnumerator SpeedBoostPowerDownRoutime()
    {
        _speed *= _speedMultiplier;

        yield return new WaitForSeconds(5);

        _speed /= _speedMultiplier;            
    }

    public void ActivateShield()
    {
        _isShieldOn = true;
        _shieldObject.SetActive(true);
    }

}
