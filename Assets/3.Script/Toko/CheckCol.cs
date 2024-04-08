using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCol : MonoBehaviour
{
    private CircleCollider2D _circleCol;
    public bool canJump = false;

    private void Awake()
    {
        TryGetComponent(out _circleCol);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(6))
        {
            canJump = true;
        }
    }
}
