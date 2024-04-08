using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    private float _speed = 7f;
    private float _xPos;

    private void Awake()
    {
        _xPos = transform.position.x;
    }


    private void FixedUpdate()
    {
        transform.position += Vector3.left * Time.fixedDeltaTime * _speed;
        if (_xPos - 20 >= transform.position.x)
        {
            transform.position = new Vector3(_xPos, 0);
        }
    }
}
