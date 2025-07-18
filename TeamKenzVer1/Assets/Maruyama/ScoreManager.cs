using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] Transform _player;
    float _startPos;
    void Start()
    {
        if (_player == null)
        {
            Debug.Log("プレーヤーがアサインされていません。");
            return;
        }
        _startPos = _player.position.y;
    }

    void Update()
    {
        float score = _player.position.y - _startPos;
        _score.text = Mathf.FloorToInt(score).ToString();
    }
}
