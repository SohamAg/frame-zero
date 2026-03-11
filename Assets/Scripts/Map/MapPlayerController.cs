using UnityEngine;
using UnityEngine.InputSystem;

public class MapPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float x = 0;
        float z = 0;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) z += 1;
            if (Keyboard.current.sKey.isPressed) z -= 1;
            if (Keyboard.current.aKey.isPressed) x -= 1;
            if (Keyboard.current.dKey.isPressed) x += 1;
        }

        Vector3 move = new Vector3(x, z, 0);

        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}