using NUnit.Framework.Constraints;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] private float speed = 5f;

    [Header("References")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform firePoint;
    PlayerController controller;
    Vector2 movement;

    [Header("ScriptReferences")]
    ObjectPooling pooler;

 
    [Header("Dashing")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    private bool isDashing = false;
    private bool canDash = true;
    private bool dashPressed = false;
    [SerializeField] TrailRenderer trailRenderer;

    float x;
    float y;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = new PlayerController();
        pooler = FindObjectOfType<ObjectPooling>();

        MovementCalling();
        Dash();
    }

    void MovementCalling()
    {
        controller.Player.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controller.Player.Move.canceled += ctx => movement = Vector2.zero;
    }

    void Dash()
    {
        controller.Player.Dash.performed += ctx => dashPressed = true;
        {
            Debug.Log("Dash pressed");
        }
    }
   

    void OnEnable()
    {
        controller.Player.Enable();
    }

    void OnDisable()
    {
        controller.Player.Disable();
    }

    private void Update()
    {
      
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
        if(isDashing)
        {
            return;
        }

        if(canDash == true && dashPressed == true)
        {
            dashPressed = false;
            StartCoroutine(Dashing());
            Debug.Log("Start Dashing");
        }
       
        Vector2 move = rb.position + movement * speed * Time.fixedDeltaTime;
        rb.MovePosition(move);
    }


    IEnumerator Dashing()
    {
        canDash = false;
        isDashing = true;
        rb.AddForce(movement * dashSpeed, ForceMode2D.Impulse);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashDuration);
        rb.linearVelocity = Vector2.zero;
        isDashing = false;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }


    void Attack()
    {
        Debug.Log("Attacking");
    }

    void Shoot()
    {
        if(pooler != null)
        {
            pooler.SpawnObjects("Bullet", firePoint.position, firePoint.rotation);
        }
    }
}