using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    public float walkSpeed = 6f;
    public float runSpeed = 10f;
    public float jumpForce = 6f;
    public LayerMask groundLayer;

    [Header("Coyote Time")]
    public float coyoteTime = 0.2f; // Hoe lang de speler nog kan springen nadat hij van de grond is gevallen
    private float coyoteTimeCounter;

    [Header("References")]
    public Rigidbody2D rb;
    public Transform groundCheck;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    [Header("Obstacles")]
    public float mudSlowdownFactor = 0.5f; // Factor waarmee de snelheid wordt verminderd in modder
    private float _currentMudMultiplier = 1f; // Huidige snelheidsvermindering door modder

    private float _horizontal;
    private bool _isGrounded;
    private bool _isRunning;

    void Update()
    {
        //input ophalen
        _horizontal = Input.GetAxisRaw("Horizontal");

        //checken of we op de grond staan
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        //Coyoto Time Logica
        if (_isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        } else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        //springen
        if (Input.GetButtonDown("Jump") && coyoteTimeCounter > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            coyoteTimeCounter = 0f; // Voorkom dubbel springen
        }

        //checken of we aan het rennen zijn
        _isRunning = Input.GetKey(KeyCode.LeftShift) && _horizontal != 0;

        float baseSpeed = _isRunning ? runSpeed : walkSpeed;
        float finalSpeed = baseSpeed * _currentMudMultiplier;


        animator.SetFloat("Speed", Mathf.Abs(_horizontal));
        animator.SetBool("IsGrounded", _isGrounded);
        animator.SetBool("IsRunning", _isRunning);

        //sprite flippen naar kijk richting
        if (_horizontal > 0)
            spriteRenderer.flipX = false;
        else if (_horizontal < 0)
            spriteRenderer.flipX = true;


        rb.linearVelocity = new Vector2(_horizontal * finalSpeed, rb.linearVelocity.y);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mud"))
        {
            _currentMudMultiplier = mudSlowdownFactor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Mud"))
        {
            _currentMudMultiplier = 1f; // Reset naar normale snelheid
        }
    }
}
