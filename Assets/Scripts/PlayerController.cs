using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    private Rigidbody2D _rb;
    public float walkSpeed;
    public float jumpForce;
    public float jumpMultiplier = .5f;
    public float normalGravity;
    public float jumpGravity;
    public float fallGravity;

    //Inputs
    [SerializeField] private Vector2 _moveInput;
    private PlayerInput _playerInput;
    private bool startJump;
    private bool cancelJump;
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool _isGround;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rb.gravityScale = normalGravity;
    }

    void Update()
    {
        FlipSprite();
    }

    void FixedUpdate()
    {
        DynamicGravity();
        CheckGround();
        HandleMovement();
        HandleJump();
    }

    public void InputMove(InputAction.CallbackContext input)
    {
        _moveInput = input.ReadValue<Vector2>();
    }

    public void InputJump(InputAction.CallbackContext input) //besok kurapiin lagi
    {
        if (input.started) 
        {
            startJump = true;
            cancelJump = false;
        }
        else if(input.canceled) 
        {
            cancelJump = true;
        }
    }

    private void HandleMovement()
    {
        float targetSpeed = _moveInput.x * walkSpeed;
        _rb.linearVelocity = new Vector2(targetSpeed, _rb.linearVelocity.y);
    }

    private void HandleJump() //besok kurapiin lagi
    {
        if (startJump && _isGround) 
        {
            startJump = false;
            cancelJump = false;
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
        }
        if (cancelJump)
        {
            cancelJump = false;
            if(_rb.linearVelocity.y > 0) _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.y * jumpMultiplier);
        }
    }

    public void DynamicGravity()
    {
        if (_rb.linearVelocity.y < -0.1f) _rb.gravityScale = fallGravity;
        else if (_rb.linearVelocity.y > 0.1f) _rb.gravityScale = jumpGravity;
        else _rb.gravityScale = normalGravity;
    }

    public void CheckGround()
    {
        _isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void FlipSprite()
    {
        if (_moveInput.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    } 

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);        
    }
}
