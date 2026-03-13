using UnityEngine;

public class FishPickup : MonoBehaviour
{
    public int fishValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Fish collected!");

            Destroy(gameObject);
        }
    }
}