using UnityEngine;

public class Combat : MonoBehaviour
{

    Animator animator;

    [Header("Attack Settings")]
    [SerializeField] int attackDamage = 10;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask enemyLayers;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("Attack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D hit in hitEnemies)
            {
                var eh = hit.GetComponent<Health>();
                if (eh != null)
                {
                    Debug.Log("Dealing damage to enemy");
                    eh.TakeDamage(10);
                    Debug.Log("Damage done to enemy ");
                }
            }

        }
    }
}
