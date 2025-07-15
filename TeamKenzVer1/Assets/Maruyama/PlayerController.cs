using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float deltaMove = 1.5f;

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


            if (Input.GetButtonDown("Jump"))
            {
                // ここで最後に向いてた方向でジャンプ（入力が0でもOK）
                Vector3 jumpDirection = _isFacingRight ? new Vector3(1f, 1.8f) : new Vector3(-1f, 1.8f);
                _rb.velocity = Vector3.zero;
                _rb.AddForce(jumpDirection.normalized * jumpPower, ForceMode2D.Impulse);
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
