using UnityEngine;

public class PlayerDefense : MonoBehaviour
{
    public bool isDefending = false;

    public float cooldownTime = 5f;
    private float nextAllowedTime = 0f;

    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            if (Time.time >= nextAllowedTime)
            {
                isDefending = true;
                Debug.Log("Shield Up");
            }
            else
            {
                Debug.Log("Shield on cooldown!");
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (isDefending)
            {
                isDefending = false;
                nextAllowedTime = Time.time + cooldownTime;
                Debug.Log("Shield Down - cooldown started");
            }
        }
    }
}