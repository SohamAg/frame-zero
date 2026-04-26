using UnityEngine;

public class RootEnemy : MonoBehaviour
{
    public Transform player;
    public float speed;

    private void Start()
    {
        speed = 10f;
    }

    void Update()
    {
        if (player == null) return;

        // 1. Look at player
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

        // 2. Move towards player
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}