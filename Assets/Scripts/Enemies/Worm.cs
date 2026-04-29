using UnityEngine;
using System.Collections;

public class Worm : Enemy
{
    public LayerMask wallLayerMask;
    private bool _hitWall;
    private float _currentDirX = 1;
    protected Rigidbody2D rb;
    [SerializeField] protected float speed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        FlipSprite();
    }

    void FixedUpdate()
    {
        CheckWall();
        MovementLogic();
    }

    protected override void MovementLogic()
    {
        rb.linearVelocity = new Vector2(_currentDirX * speed, rb.linearVelocity.y);
        _hitWall = false;
    }

    private void CheckWall()
    {
        Vector2 _currentDir = new Vector2(_currentDirX, 0f); 
        _hitWall = Physics2D.Raycast(transform.position, _currentDir, .8f, wallLayerMask);
        if(_hitWall) _currentDirX *= -1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 _currentDir = new Vector2(_currentDirX, 0f); 
        Gizmos.DrawRay(transform.position, _currentDir * .8f);        
    }

    private void FlipSprite()
    {
        if (_currentDirX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_currentDirX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    } 
}
