using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public int damage = 5;
    public float attackCooldown = 1.2f;

    private PlayerHealth playerHealth;
    private float lastAttackTime;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    public void DealDamage()
    {
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        if (playerHealth == null) return;

        Debug.Log("Damage dealt: " + damage);

        playerHealth.TakeDamage(damage);

        lastAttackTime = Time.time;
    }
}