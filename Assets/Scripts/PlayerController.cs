using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    public Rigidbody2D rb;
    public float speed;

    public Vector2 moveInput;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        FlipSprite();
    }

    void FixedUpdate()
    {
        float targetSpeed = moveInput.x * speed;
        rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y);
    }

    public void Move(InputAction.CallbackContext input)
    {
        Debug.Log("Move input = " + input.ReadValue<Vector2>());
        moveInput = input.ReadValue<Vector2>();
    }

    public void FlipSprite()
    {
        if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
