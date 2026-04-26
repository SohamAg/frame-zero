using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage = 10f;
    public float attackRange = 2.5f;
    public float attackCooldown = 0.5f;

    public Transform attackPoint;
    public LayerMask enemyLayer;

    private float lastAttackTime;

    private PlayerDefense defense;

    void Start()
    {
        defense = GetComponent<PlayerDefense>(); // get shield script
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryAttack();
        }
    }

    void TryAttack()
    {

        if (defense != null && defense.isDefending)
            return;

        if (Time.time < lastAttackTime + attackCooldown)
            return;

        lastAttackTime = Time.time;

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