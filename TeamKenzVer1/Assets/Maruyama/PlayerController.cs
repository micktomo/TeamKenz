using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] float moveSpeed = 3f;

    bool _isGrounded = false;
    bool _isFacingRight = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        // 向き切り替えは入力にかかわらず一度記憶したら維持
        if (moveInput < 0)
        {
            _isFacingRight = false;
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
        else if (moveInput > 0)
        {
            _isFacingRight = true;
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        if (_isGrounded) // 接地中
        {
            // 接地中のみ横移動
            _rb.velocity = new Vector3(moveInput * moveSpeed, _rb.velocity.y);

            if (Input.GetButtonDown("Jump"))
            {
                // ここで最後に向いてた方向でジャンプ（入力が0でもOK）
                Vector3 jumpDirection = _isFacingRight ? new Vector3(2f, 1.5f) : new Vector3(-2f, 1.5f);
                // Debug.Log(jumpDirection);
                // _rb.velocity = Vector2.zero;
                Debug.Log(jumpDirection.normalized * jumpPower);
                //_rb.AddForce(jumpDirection.normalized * jumpPower, ForceMode2D.Impulse);
                _rb.AddForce(new Vector2(10, 10), ForceMode2D.Impulse);
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
