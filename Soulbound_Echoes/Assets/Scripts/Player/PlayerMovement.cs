using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;

    public PlayerIdle idleState;
    
    public PlayerJump jumpState;

    public PlayerMove moveState;

    public PlayerCrouch crouchState;

    public PlayerSlide slideState;


    [Header("References")]  
    public Rigidbody2D rb;
    [SerializeField] PlayerInput playerInput;
    public Animator animator;
    [SerializeField] private CapsuleCollider2D capsuleCollider;

    [Header("Movement Settings")]
    public  float walkSpeed = 5f;
    public float runSpeed = 8f;
    public int facingDirection = 1;

    [Header("Input Values")]
    public Vector2 moveInput;
    public bool jumpPressed;
    public bool jumpReleased;
    public bool runPressed;

    [Header("Jump Settings")]
    public float jumpForce = 10f;
    public float jumpMultiplier = 0.5f;
    [SerializeField] private float normalGravityScale = 1f;
    [SerializeField] private float fallGravityScale = 2.5f;
    [SerializeField] private float jumpGravityScale = 2f;

    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private LayerMask whatIsGround;
    public bool isGrounded;


    [Header("Crouch Settings")]
    [SerializeField] Transform ceilingCheck;
    [SerializeField] float ceilingCheckRadius = 0.2f;

    [Header("Slide Settings")]
    public float slideDuration = 0.6f;
    public float slideSpeed = 12f;
    public float slideStopDuration = 0.5f;

    public float slideHeight;
    public Vector2 slideOffSet;
    public float normalHeight;
    public Vector2 normalOffSet;

    private bool isSliding;


    private void Awake()
    {
        idleState = new PlayerIdle(this);
        jumpState = new PlayerJump(this);
        moveState = new PlayerMove(this);
        crouchState = new PlayerCrouch(this);
        slideState = new PlayerSlide(this);
    }

    private void Start()
    {
        rb.gravityScale = normalGravityScale;

        ChangeState(idleState);
    }

    private void Update()
    {
        currentState.Update();

        if (!isSliding)
        {
            Flip();
        }
        HandleAnimations();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate();
        CheckGrounded();
    }

    public void ChangeState(PlayerState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnRun(InputValue value)
    {
        runPressed = value.isPressed;
    }


    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            jumpPressed = true;
            jumpReleased = false;
        }
        else if (rb.linearVelocity.y > 0)
        {
            jumpReleased = true;
        }
    }

    public void ApplyVariableJump()
    {
        if(rb.linearVelocity.y < -0.1f)
        {
            rb.gravityScale = fallGravityScale;
        }
        else if (rb.linearVelocity.y > 0.1f) 
        {
            rb.gravityScale = jumpGravityScale;
        }
        else
        {
            rb.gravityScale = normalGravityScale;
        }
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    public bool CheckForCieling()
    {
       return  Physics2D.OverlapCircle(ceilingCheck.position, ceilingCheckRadius, whatIsGround);
    }

    public void SetColliderNormal()
    {
        capsuleCollider.size = new Vector2(capsuleCollider.size.x, normalHeight);
        capsuleCollider.offset = normalOffSet;
    }

    public void SetColliderSlide()
    {
        capsuleCollider.size = new Vector2(capsuleCollider.size.x, slideHeight);
        capsuleCollider.offset = slideOffSet;
    }


    void HandleAnimations()
    {
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("yVelocity", rb.linearVelocity.y < -0.1f);
    }

    void Flip()
    {
        if (moveInput.x > 0.1f)
        {
            facingDirection = 1;
        }
        else if (moveInput.x < -0.1f)
        {
            facingDirection = -1;
        }
        transform.localScale = new Vector3(facingDirection, 1, 1);
    }
    /*
 

    [Header("AttackReferences")]
    [SerializeField] Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int attackDamage = 10;
  

    private void FixedUpdate()
    {
       Attack();
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
    */
}