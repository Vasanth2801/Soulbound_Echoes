using UnityEngine;

public class Combat : MonoBehaviour
{
    public PlayerMovement player;


    [Header("AttackReferences")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackCooldown = 1f;
    public LayerMask enemyLayer;
    public int attackDamage = 10;
    private float nextAttackTime;
    public Animator hitFX;
    public bool canAttack => Time.time >= nextAttackTime;

    public void AttackAnimationFinished()
    {
        player.AttackAnimationFinished();
    }

    public void Attack()
    {
        if(!canAttack)
        {
            return;
        }

        nextAttackTime = Time.time + attackCooldown;

        Collider2D enemy = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayer);

        if (enemy != null)
        {
            hitFX.Play("HITFX");
            enemy.gameObject.GetComponent<Health>().ChangeHealth(-attackDamage);
        }
    }
}
