using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;
    private Animator _animator;
    [SerializeField]
    private AudioClip _explosionSoundClip;
    private AudioSource _audioSource;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }

        _animator = gameObject.GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator is Null");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Enemy audio source is null");
        }
        else
        {
            _audioSource.clip = _explosionSoundClip;
        }
    }
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5.5f)
        {
            float randomX = Random.Range(-9.0f, 9.0f);
            transform.position = new Vector3(randomX, 7.5f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                _player = other.transform.GetComponent<Player>();
                if (_player != null)
                {
                    _animator.SetTrigger("OnEnemyDeath");
                    _player.Damage();
                }
                _speed = 0f;
                _audioSource.Play();
                Destroy(this.gameObject, 2.5f);
                break;
            case "Laser":
                if (_player != null)
                {
                    _player.AddScore(10);
                }
                _speed = 0f;
                _animator.SetTrigger("OnEnemyDeath");
                _audioSource.Play();
                Destroy(other.gameObject);
                Destroy(this.gameObject, 2.5f);
                break;
            default:
                break;
        }
    }
}
