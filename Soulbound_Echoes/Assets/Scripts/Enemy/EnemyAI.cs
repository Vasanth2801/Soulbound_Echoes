using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private float enemySpeed = 3f;
    [SerializeField] Transform player;

    private void Update()
    {
        Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, enemySpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Damaging the Player");
        }
    }
}