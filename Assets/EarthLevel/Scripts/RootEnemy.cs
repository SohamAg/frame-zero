using UnityEngine;

public class RootEnemy : MonoBehaviour
{
    public Transform player;
    public float speed;

    // distance threshold for ai states
    private float slowThreshold = 25f;
    private float fastThreshold = 10f;

    // speeds for states
    private float fastSpeed = 12f;
    private float slowSpeed = 10f;
    private float patrolSpeed = 3f;
    public enum AIState
    {
        Patrol,
        ChaseSlow,
        ChaseFast
    };
    public AIState aiState;

    private void Start()
    {
        aiState = AIState.Patrol;
        speed = 2f;
    }

    void Update()
    {
        if (player == null) return;

        // Look at player
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

        // calculate distance from player to determine state
        float distance_from_player = Vector3.Distance(transform.position, player.position);

        switch (aiState)
        {
            
            case AIState.Patrol:
                speed = patrolSpeed;
                if (distance_from_player < fastThreshold)
                {
                    aiState = AIState.ChaseFast;
                }
                else if (distance_from_player < slowThreshold)
                {
                    aiState = AIState.ChaseSlow;
                }
                break;

            case AIState.ChaseSlow:
                speed = slowSpeed;
                if (distance_from_player < fastThreshold)
                {
                    aiState = AIState.ChaseFast;
                }
                if (distance_from_player > slowThreshold)
                {
                    aiState = AIState.Patrol;
                }
                break;

            case AIState.ChaseFast:
                speed = fastSpeed;
                if (distance_from_player > slowThreshold)
                {
                    aiState = AIState.Patrol;
                }
                else if (distance_from_player > fastThreshold)
                {
                    aiState = AIState.ChaseSlow;
                }
                break;
            default:
                speed = 2f;
                aiState = AIState.Patrol;
                break;
        }
        

        // Move towards player
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}