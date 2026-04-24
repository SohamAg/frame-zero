using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage = 10f;
    public float attackRange = 2.5f;
    public float attackCooldown = 0.5f;

    public Transform attackPoint; // where ray starts (usually camera or player chest)
    public LayerMask enemyLayer;

    private float lastAttackTime;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click attack
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        lastAttackTime = Time.time;

        // Raycast forward
        RaycastHit hit;
        if (Physics.Raycast(attackPoint.position, attackPoint.forward, out hit, attackRange, enemyLayer))
        {
            MonsterHealth boss = hit.collider.GetComponent<MonsterHealth>();

            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
        }
    }
}