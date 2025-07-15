using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] float jumpPower = 3f;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float deltaMove = 1.5f;
    [SerializeField] float maxChargeTime = 1f;
    float chargeTime = 0f;

    bool _isGrounded = false;
    bool _isFacingRight = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (_isGrounded) // 接地中
        {
            // 接地中のみ横移動
            //_rb.velocity = new Vector3(moveInput * moveSpeed, _rb.velocity.y);
            if (moveInput > 0)
            {
                _isFacingRight = true;
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                transform.position += new Vector3(deltaMove * Time.deltaTime, 0, 0);
            }
            else if (moveInput < 0)
            {
                _isFacingRight = false;
                transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
                transform.position -= new Vector3(deltaMove * Time.deltaTime, 0, 0);
            }

            /*
            if (Input.GetButtonDown("Jump"))
            {
                // 向いている方向でジャンプ（入力なしでも transform.localScale 依存）
                // TODO ジャンプキーの押されている時間依存でベクトルの向きと大きさを決定したい
                Vector3 jumpDirection = _isFacingRight ? new Vector3(1f, 1.8f) : new Vector3(-1f, 1.8f);
                _rb.velocity = Vector3.zero;
                _rb.AddForce(jumpDirection.normalized * jumpPower, ForceMode2D.Impulse);
            }*/

            if (Input.GetButton("Jump")) // Jumpキーの入力があるとき
            {
                chargeTime += Time.deltaTime;
                if (chargeTime > maxChargeTime)
                {
                    chargeTime = maxChargeTime;
                }
            }

            if (Input.GetButtonUp("Jump")) // Jumpキーがリリースされたら
            {
                // 実際のチャージ時間と最大チャージ時間を0～1で返す
                float chargeGage = Mathf.Clamp01(chargeTime / maxChargeTime);
                // 上記を用いて角度を調整（ここでは20～70）
                float radValue = Mathf.Lerp(20f, 70f, chargeGage);
                // 度数法（Rad）として変換
                float rad = radValue * Mathf.Deg2Rad;
                // ジャンプの大きさも同様に調整
                float power = Mathf.Lerp(jumpPower * 0.5f, jumpPower, chargeGage);
                float dirX = Mathf.Cos(rad) * (_isFacingRight ? 1 : -1);
                float dirY = Mathf.Sin(rad);
                Vector3 jumpDirection = new Vector3(dirX, dirY, 0f);
                _rb.velocity = Vector3.zero;
                _rb.AddForce(jumpDirection.normalized * power, ForceMode2D.Impulse);

                chargeTime = 0; // チャージタイムリセット
            }
        }
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
