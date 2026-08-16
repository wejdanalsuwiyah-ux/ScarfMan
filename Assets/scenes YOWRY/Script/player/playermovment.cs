using UnityEngine;
using UnityEngine.InputSystem;
public class playermovment : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();

    }


    void Update()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            input.y += 1;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            input.y -= 1;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            input.x += 1;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            input.x -= 1;
        }

        Vector3 move = new Vector3(input.x, 0f, input.y);
        move = move.normalized;
        controller.Move(move * moveSpeed * Time.deltaTime);
        if (move != Vector3.zero)
        {
            transform.forward = move;
        }
    }
}
