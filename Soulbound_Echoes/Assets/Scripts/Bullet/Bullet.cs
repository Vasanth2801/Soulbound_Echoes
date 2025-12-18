using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Player Power settings")]
    [SerializeField] private float bulletSpeed = 2f;
    [SerializeField] private float timer;
    [SerializeField] private float lifeTime = 4f;


    [SerializeField] Rigidbody2D rb;


    void Start()
    {
        timer = lifeTime;
    }

    void OnEnable()
    {
        if (rb != null)
        {
            rb.linearVelocity = transform.up * bulletSpeed;
        }
    }

    void Update()
    {
        if(timer >= 0)
        {
            timer -= Time.deltaTime;
        }

        if(timer == 0)
        {
            gameObject.SetActive(false);
            lifeTime = timer;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }
}
