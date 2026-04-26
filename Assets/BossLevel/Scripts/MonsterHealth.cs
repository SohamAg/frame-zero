using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MonsterHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    private Animator animator;
    private NavMeshAgent agent;

    public Slider healthBar;

    [Header("Death Reward")]
    public GameObject crystalInScene;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (healthBar != null)
            healthBar.value = currentHealth;

        animator.SetFloat("Health", currentHealth);

        if (currentHealth > 0)
        {
            animator.SetTrigger("Hit");
        }
        else
        {
            StartCoroutine(DieRoutine());
        }
    }

    System.Collections.IEnumerator DieRoutine()
    {
        if (isDead) yield break;
        isDead = true;


        if (agent != null)
            agent.enabled = false;

        // play death animation
        animator.SetTrigger("Death");

        if (crystalInScene != null)
            crystalInScene.SetActive(true);


        yield return new WaitForSeconds(GetDeathAnimationLength());


        Destroy(gameObject);
    }

    float GetDeathAnimationLength()
    {

        AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);

        if (clips.Length > 0)
            return clips[0].clip.length;

        return 2f; 
    }
}