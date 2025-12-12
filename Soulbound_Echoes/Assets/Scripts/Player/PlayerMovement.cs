using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] private float speed = 5f;

    [Header("References")]
    [SerializeField] Rigidbody2D rb;
    float x;
    float y;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if(Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity  = new Vector2(x * speed , y  * speed);
    }

    void Attack()
    {
        Debug.Log("Attacking");
    }

    void Shoot()
    {
        Debug.Log("Shooting");
    }
}