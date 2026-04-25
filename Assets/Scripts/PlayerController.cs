using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    public float jumpMultiplier = .5f;
    public float normalGravity;
    public float jumpGravity;
    public float fallGravity;
    private Rigidbody2D _rb;

    //Inputs
    [SerializeField] private Vector2 _moveInput;
    private PlayerInput _playerInput;
    private bool _startJump;
    private bool _cancelJump;
    private bool _isRunning;
    //Animations
    private Animator _animator;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool _isGround;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        _rb.gravityScale = normalGravity;
    }

    void Update()
    {
        FlipSprite();
        HandleAnimation();
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
            _startJump = true;
            _cancelJump = false;
        }
        else if(input.canceled) 
        {
            _cancelJump = true;
        }
    }

    public void InputRun(InputAction.CallbackContext input)
    {
        Debug.Log(input.ReadValueAsButton());
        _isRunning = input.ReadValueAsButton();
    }

    private void HandleMovement()
    {
        float currentSpeed = _isRunning ? runSpeed : walkSpeed;
        float targetSpeed = _moveInput.x * currentSpeed;
        _rb.linearVelocity = new Vector2(targetSpeed, _rb.linearVelocity.y);
    }

    private void HandleJump() //besok kurapiin lagi
    {
        if (_startJump && _isGround) 
        {
            _startJump = false;
            _cancelJump = false;
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
        }
        if (_cancelJump)
        {
            _cancelJump = false;
            if(_rb.linearVelocity.y > 0) _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.y * jumpMultiplier);
        }
    }

    public void HandleAnimation()// ntar/besok/kapan-kapan ku rapiin lagi
    {
        _animator.SetBool("isIdle", Mathf.Abs(_moveInput.x) < .1f && _isGround);
        _animator.SetBool("isWalking", Mathf.Abs(_moveInput.x) > .1f && _isGround && !_isRunning);
        _animator.SetBool("isRunning", Mathf.Abs(_moveInput.x) > .1f && _isGround && _isRunning);

        _animator.SetBool("isJumping", _rb.linearVelocity.y > .1f);
        _animator.SetBool("isGround", _isGround);
        _animator.SetFloat("yVelocity", _rb.linearVelocity.y);
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
