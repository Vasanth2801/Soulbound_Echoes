using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerMovement Settings")]
    public float speed = 5f;            
    public float jumpForce = 9f;   
    float moveInputX;

    [Header("Ground Check Settings")]
    private bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask whatIsGround;

    [Header("References")]
    private Rigidbody2D rb;

    [Header("Bool for the players")]
    public bool isFacingRight = true;
    bool doubleJump;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();            
    }

    void Update()
    {
        moveInputX = Input.GetAxisRaw("Horizontal");

        Jump();

      
        Flip();                    
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInputX * speed, rb.linearVelocity.y);    
    }

    void Flip()
    {
        if (isFacingRight && moveInputX < 0 || !isFacingRight && moveInputX > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;               
            localScale.x *= -1;                                     
            transform.localScale = localScale;                      
        }
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            doubleJump = true;
        }
        else if(doubleJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * 0.9f);
            doubleJump = false;
        }
    }
}