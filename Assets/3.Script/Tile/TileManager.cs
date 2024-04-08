using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance = null;

    [SerializeField] private GameObject _tile;

    public Queue<GameObject> _tiles = new Queue<GameObject>();
    public Transform _tileSpawnPoint;

    private float _spawnTime = 1.2f;
    private int _spawnCount = 7;

    private IEnumerator TileSpawn_co;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else return;
    }

    public void TileSetting()
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            GameObject tile = Instantiate(_tile, RandomSpawnPos(), Quaternion.identity);
            tile.SetActive(false);
            _tiles.Enqueue(tile);
        }
    }

    public Vector3 RandomSpawnPos()
    {
        float random = Random.Range(-3f, 1f);
        Vector3 randomPos = _tileSpawnPoint.position + (Vector3.up * random);
        return randomPos;
    }

    public void TileSpawn()
    {
        TileSpawn_co = TileSpawn_Co();
        StartCoroutine(TileSpawn_co);
    }

    private IEnumerator TileSpawn_Co()
    {
        while (GameManager.Instance.startedGame)
        {
            var tile = _tiles.Dequeue();
            tile.SetActive(true);
            yield return new WaitForSeconds(_spawnTime);
        }
    }
}
