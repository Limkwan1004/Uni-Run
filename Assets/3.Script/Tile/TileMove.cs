using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class TileMove : MonoBehaviour
{
    private float _speed = 13f;

    private void FixedUpdate()
    {
        transform.position += Vector3.left * Time.fixedDeltaTime * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dead"))
        {
            TileManager.Instance._tiles.Enqueue(gameObject);
            gameObject.transform.position = TileManager.Instance.RandomSpawnPos();
            gameObject.SetActive(false);
        }
    }
}
