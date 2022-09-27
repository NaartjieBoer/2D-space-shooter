using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField] // 0 = Tripple; 1 = Speed; 2 = Shield;
    private int _powerupID;
    void Update()
    {
        transform.Translate(Vector2.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                switch (_powerupID)
                {
                    case 0:
                        player.ActivateTrippleShot();
                        break;
                    case 1:
                        player.ActivateSpeedBoost();
                        break;
                    case 2:
                        Debug.Log("Shield");
                        break;
                    default:
                        Debug.Log("Powerup = default value");
                        break;
                }
            }

            Destroy(this.gameObject);
        }
    }
}
