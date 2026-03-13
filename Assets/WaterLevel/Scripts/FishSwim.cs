using UnityEngine;

public class FishSwim : MonoBehaviour
{
    public float swimSpeed = 2f;
    public float changeDirectionTime = 3f;

    public Transform pondCenter;
    public float pondRadius = 6f;

    private Vector3 swimDirection;
    private float timer;

    private bool collected = false;

    void Start()
    {
        PickNewDirection();
    }

    void Update()
    {
        if (collected) return;

        // Move fish
        transform.Translate(swimDirection * swimSpeed * Time.deltaTime, Space.World);

        // Rotate fish toward direction
        if (swimDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(swimDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        }

        timer += Time.deltaTime;

        if (timer >= changeDirectionTime)
        {
            PickNewDirection();
            timer = 0f;
        }

        KeepFishInPond();
    }

    void PickNewDirection()
    {
        swimDirection = new Vector3(
            Random.Range(-1f, 1f),
            0,
            Random.Range(-1f, 1f)
        ).normalized;
    }

    void KeepFishInPond()
    {
        if (pondCenter == null) return;

        float distance = Vector3.Distance(transform.position, pondCenter.position);

        if (distance > pondRadius)
        {
            Vector3 directionBack = (pondCenter.position - transform.position).normalized;
            swimDirection = directionBack;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            Debug.Log("Fish collected!");

            gameObject.SetActive(false);
        }
    }
}