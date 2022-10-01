using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
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
                    _player.Damage();
                }

                Destroy(this.gameObject);
                break;
            case "Laser":
                if (_player != null)
                {
                    _player.AddScore(10);
                }

                Destroy(other.gameObject);
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
}
