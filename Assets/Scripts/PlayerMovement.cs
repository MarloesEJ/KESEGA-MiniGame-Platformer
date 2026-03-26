using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 8f;
    public float jumpForce = 5f;
    public LayerMask groundLayer;

    [Header("References")]
    public Rigidbody2D rb;
    public Transform groundCheck;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private float _horizontal;
    private bool _isGrounded;

    void Update()
    {
        //input ophalen
        _horizontal = Input.GetAxisRaw("Horizontal");

        //checken of we op de grond staan
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        //springen
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        animator.SetFloat("Speed", Mathf.Abs(_horizontal));
        animator.SetBool("IsGrounded", _isGrounded);

        //sprite flippen naar kijk richting
        if (_horizontal > 0)
            spriteRenderer.flipX = false;
        else if (_horizontal < 0)
            spriteRenderer.flipX = true;

    }

    void FixedUpdate()
    {
        //bewegen
        rb.linearVelocity = new Vector2(_horizontal * speed, rb.linearVelocity.y);
    }
}
