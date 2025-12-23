using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerMovement Settings")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float jumpForce = 9f;   
    float moveInputX;

    [Header("Ground Check Settings")]
    private bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask whatIsGround;

    [Header("AttackReferences")]
    [SerializeField] Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int attackDamage = 10;

    [Header("References")]
    private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [Header("Bool for the players")]
    [SerializeField] bool isFacingRight = true;
    [SerializeField] bool doubleJump;
    bool runPressed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();     
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        moveInputX = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.J))
        {
            runPressed = true;
        }

        if(Input.GetKeyUp(KeyCode.J))
        {
            runPressed = false;
        }

        HandleAnimations();

       

        Jump();

        Flip();                    
    }

    private void FixedUpdate()
    {
        PlayerMove();
        Attack();
    }

    void PlayerMove()
    {
        float currentSpeed = runPressed ? runSpeed : walkSpeed;
        rb.linearVelocity = new Vector2(moveInputX * currentSpeed, rb.linearVelocity.y);
    }


    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("Attack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D hit in hitEnemies)
            {
                
                var eh = hit.GetComponent<Health>();
                if (eh != null)
                {
                    eh.TakeDamage(attackDamage);
                    Debug.Log("Damage done to enemy ");
                }
            }
        }
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

    void HandleAnimations()
    {
        animator.SetBool("isJumping", rb.linearVelocity.y >0.1f);
        animator.SetBool("isGrounded", isGrounded);

        animator.SetBool("yVelocity", rb.linearVelocity.y < -0.1f);

        bool isMoving = Mathf.Abs(moveInputX) > 0.1f && isGrounded;

        animator.SetBool("isIdle",!isMoving && isGrounded);
        animator.SetBool("isWalking", isMoving && !runPressed);
        animator.SetBool("isRunning", isMoving && runPressed);
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        else if (!isGrounded && Input.GetButtonDown("Jump") && !doubleJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            doubleJump = true;
        }

        if (isGrounded)
        {
            doubleJump = false;
        }
    }
}