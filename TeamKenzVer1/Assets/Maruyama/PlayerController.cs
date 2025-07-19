using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] float _jumpPower = 15f;
    [SerializeField] float _deltaMove = 1.5f;
    [SerializeField] float _maxChargeTime = 1f;
    [SerializeField] float _minAngle = 25f;
    [SerializeField] float _maxAngle = 70f;
    float _chargeTime = 0f;
    bool _isGrounded = false;
    bool _isFacingRight = true;
    [SerializeField] float _playerScaleX = 0.5f;
    [SerializeField] float _playerScaleY = 0.5f;
    [SerializeField] float _playerScaleZ = 0.5f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (_isGrounded)
        {
            if (moveInput > 0)
            {
                _isFacingRight = true;
                transform.localScale = new Vector3(_playerScaleX, _playerScaleY, _playerScaleZ);
                transform.position += new Vector3(_deltaMove * Time.deltaTime, 0, 0);
            }
            else if (moveInput < 0)
            {
                _isFacingRight = false;
                transform.localScale = new Vector3(-1 * _playerScaleX, _playerScaleY, _playerScaleZ);
                transform.position -= new Vector3(_deltaMove * Time.deltaTime, 0, 0);
            }

            if (Input.GetButton("Jump"))
            {
                _chargeTime += Time.deltaTime;
                if (_chargeTime > _maxChargeTime)
                {
                    _chargeTime = _maxChargeTime;
                }
            }

            if (Input.GetButtonUp("Jump"))
            {
                Jump(_jumpPower, _minAngle, _maxAngle);
            }
        }
    }

    void Jump(float jumpPower, float minAngle, float maxAngle)
    {
        // チャージ時間と最大チャージ時間を0～1で返す
        float chargeGage = Mathf.Clamp01(_chargeTime / _maxChargeTime);
        float radValue = Mathf.Lerp(minAngle, maxAngle, chargeGage);
        float rad = radValue * Mathf.Deg2Rad;

        float power = Mathf.Lerp(jumpPower * 0.5f, jumpPower, chargeGage);
        float dirX = Mathf.Cos(rad) * (_isFacingRight ? 1 : -1);
        float dirY = Mathf.Sin(rad);

        Vector3 jumpDirection = new Vector3(dirX, dirY, 0f);
        _rb.velocity = Vector3.zero;
        _rb.AddForce(jumpDirection.normalized * power, ForceMode2D.Impulse);

        _chargeTime = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}
