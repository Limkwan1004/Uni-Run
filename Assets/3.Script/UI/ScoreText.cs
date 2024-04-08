using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void FixedUpdate()
    {
        _scoreText.text = $"SCORE : {(int)GameManager.Instance._score}";
    }
}
