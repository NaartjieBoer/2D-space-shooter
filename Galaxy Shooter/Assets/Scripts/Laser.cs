using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;

    void Update()
    {
        //move laser up
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //destroy laser
        if (transform.position.y > 8)
        {
            Destroy(this.gameObject);

            if (this.gameObject.transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
