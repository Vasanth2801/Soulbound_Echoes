using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerMovement Settings")]
    public float speed = 5f;            
    public float jumpForce = 9f;   
    float moveInputX;                    

    [Header("References")]
    private Rigidbody2D rb;            

    [Header("Bool for the players")]
    public bool isFacingRight = true;       
    private bool isJumping = false;        

    [Header("PlayerHealthSettings")]
    public int maxHealth = 100;            
    public int currentHealth;            

   
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();            
    }


    
    private void Start()
    {
        currentHealth = maxHealth;                       
    }


  
    private void Update()
    {
        moveInputX = Input.GetAxisRaw("Horizontal");       


        //Jumping Logic 
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);  
            isJumping = true;                      
        }

        Flip();                    
    }


    
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInputX * speed, rb.linearVelocity.y);    
    }


    public void Flip()
    {
        if (isFacingRight && moveInputX < 0 || !isFacingRight && moveInputX > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;               
            localScale.x *= -1;                                     
            transform.localScale = localScale;                      
        }
    }

   
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && isJumping)      
        {
            isJumping = false;                                      
        }
    }
   
}