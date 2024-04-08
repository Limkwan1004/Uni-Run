using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public float _score { get; private set; }

    public bool startedGame { get; private set; } = false;

    private IEnumerator GameScore_co;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else return;
        Time.timeScale = 0f;
    }

    public void GameStart()
    {
        startedGame = true;
        Time.timeScale = 1f;
        StartGameScore();
        TileManager.Instance.TileSetting();
        TileManager.Instance.TileSpawn();
    }

    public void GameEnd()
    {
        startedGame = false;
    }

    private void StartGameScore()
    {
        GameScore_co = GameScore_Co();
        StartCoroutine(GameScore_co);
    }

    private IEnumerator GameScore_Co()
    {
        _score = 0;
        while (startedGame)
        {
            _score += Time.fixedDeltaTime * 40f;
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
}
