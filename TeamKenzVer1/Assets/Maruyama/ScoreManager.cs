using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] Transform _player;
    float _startPos;
    float _highScore = 0f;
    void Start()
    {
        if (_player == null)
        {
            return;
        }
        _startPos = _player.position.y;
    }

    void Update()
    {
        float currentScore = _player.position.y - _startPos;
        if (currentScore > _highScore)
        {
            _highScore = currentScore;
        }
        int scoreInt = Mathf.FloorToInt(_highScore);
        _scoreText.text = $"Score: {scoreInt}m";
    }
}
