using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Health health;

    void OnEnable()
    {
        health.OnDamaged += HandleDamaged;
        health.OnDeath += HandleDeath;
    }

    void OnDisable()
    {
        health.OnDamaged -= HandleDamaged;
        health.OnDeath -= HandleDeath;
    }

    void HandleDamaged()
    {
        Debug.Log("Enemy Damaged");
        animator.SetTrigger("Damaged");
    }

    void HandleDeath()
    {
      //  animator.SetTrigger("Death");
    }
}
