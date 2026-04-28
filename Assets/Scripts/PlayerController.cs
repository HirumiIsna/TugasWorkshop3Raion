using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    public float walkSpeed;
    public float runSpeed;
    private Rigidbody2D _rb;

    [Header("Player Jump")]
    public float jumpForce;
    public float jumpMultiplier = .5f;
    public float normalGravity;
    public float jumpGravity;
    public float fallGravity;
    public float coyoteTime = .2f;
    private float _coyoteTimeCounter;
    
    [Header("Player Attack")]
    public int attackDamage;
    public float attackRange;
    public Transform sideAttackPoint, upAttackPoint, downAttackPoint;
    public LayerMask attackableLayer;
    public LayerMask groundLayer;
    private bool _isAttacking;
    private bool _isGround;

    //Inputs
    [SerializeField] private Vector2 _moveInput;
    private PlayerInput _playerInput;
    private bool _startJump;
    private bool _cancelJump;
    private bool _isRunning;
    //Animations
    private Animator _animator;


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
        _isRunning = input.ReadValueAsButton();
    }

    public void InputAttack(InputAction.CallbackContext input) //paling kurapiin lagi tergantung mood
    {
        if (input.started && !_isAttacking)
        {
            _animator.SetTrigger("isAttacking");

            if(_moveInput.y > 0.1f) HandleAttack(upAttackPoint);
            else if(_moveInput.y < -0.1f && !_isGround) HandleAttack(downAttackPoint);
            else {
                _isAttacking = true;
                HandleAttack(sideAttackPoint);
            }
        }
    }

    private void HandleMovement()
    {
        float currentSpeed = _isRunning ? runSpeed : walkSpeed;
        float targetSpeed = _moveInput.x * currentSpeed;
        _rb.linearVelocity = new Vector2(targetSpeed, _rb.linearVelocity.y);
    }

    private void HandleJump() //besok kurapiin lagi
    {
        if (_isGround) _coyoteTimeCounter = coyoteTime;
        else _coyoteTimeCounter -= Time.deltaTime;

        if (_startJump && _coyoteTimeCounter > 0) 
        {
            _startJump = false;
            _cancelJump = false;
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
        }
        if (_cancelJump)
        {
            _cancelJump = false;
            _coyoteTimeCounter = 0f;
            if(_rb.linearVelocity.y > 0) _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.y * jumpMultiplier);
        }
    }

    private void HandleAttack(Transform point)
    {
        StartCoroutine(AttackDebounce());
        Collider2D[] hit = Physics2D.OverlapCircleAll(point.position, attackRange, attackableLayer);

        foreach (Collider2D enemy in hit)
        {
            enemy.GetComponent<Health>()?.ChangeHealth(-attackDamage);
        }

        if (point == downAttackPoint && hit.Length > 0)
        {
            Knockback();
        }
    }

    private void HandleAnimation()// ntar/besok/kapan-kapan ku rapiin lagi + benerin
    {
        _animator.SetBool("isIdle", Mathf.Abs(_moveInput.x) < .1f && _isGround && !_isAttacking);
        _animator.SetBool("isWalking", Mathf.Abs(_moveInput.x) > .1f && _isGround && !_isRunning);
        _animator.SetBool("isRunning", Mathf.Abs(_moveInput.x) > .1f && _isGround && _isRunning);

        _animator.SetBool("isJumping", _rb.linearVelocity.y > .1f);
        _animator.SetBool("isGround", _isGround);
        _animator.SetFloat("yVelocity", _rb.linearVelocity.y);
    }

    private void DynamicGravity()
    {
        if (_rb.linearVelocity.y < -0.1f) _rb.gravityScale = fallGravity;
        else if (_rb.linearVelocity.y > 0.1f) _rb.gravityScale = jumpGravity;
        else _rb.gravityScale = normalGravity;
    }

    private void CheckGround()
    {
        _isGround = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundLayer);
    }

    public void Knockback() //ngetes pogo ntar klo dah bener ku jadiin universal
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
    }
    private void FlipSprite()
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
        Gizmos.DrawRay(transform.position, Vector2.down * 1.5f);    
        Gizmos.DrawWireSphere(sideAttackPoint.position, attackRange);
        Gizmos.DrawWireSphere(upAttackPoint.position, attackRange);
        Gizmos.DrawWireSphere(downAttackPoint.position, attackRange);    
    }

    private IEnumerator AttackDebounce() //ntar kupindah/kuganti paling, cuma buat benerin animationnya doang
    {
        yield return new WaitForSeconds(0.5f);
        _isAttacking = false;
    }
}
