using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    Rigidbody2D _rb;

    Vector3 _startPos;
    float _moveRange;
    float _moveSpeed = 0.5f;
    float _moveRangeRatio = 0.4f;

    bool _isGrounded = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPos = transform.position;
    }

    void FixedUpdate()
    {
        if (_isGrounded)
        {
            MoveWithSinWave();
        }
    }
    void MoveWithSinWave()
    {
        float x = Mathf.Sin(Time.time * _moveSpeed) * _moveRange;
        Vector3 targetPos = new Vector3(_startPos.x + x, _startPos.y, _startPos.z);
        _rb.MovePosition(targetPos);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;

            Collider2D groundScale = collision.collider;
            if (groundScale != null)
            {
                float groundWidth = groundScale.bounds.size.x;
                _moveRange = groundWidth * _moveRangeRatio;
            }
        }
    }
}
