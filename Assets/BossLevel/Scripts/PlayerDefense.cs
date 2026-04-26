using UnityEngine;

public class PlayerDefense : MonoBehaviour
{
    public bool isDefending = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right click
        {
            isDefending = true;
            Debug.Log("Shield Up");
        }

        if (Input.GetMouseButtonUp(1))
        {
            isDefending = false;
            Debug.Log("Shield Down");
        }
    }
}