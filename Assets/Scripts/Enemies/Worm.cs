using UnityEngine;
using System.Collections;

public class Worm : Enemy
{
    public float speed;
    public LayerMask wallLayerMask;
    private bool _hitWall;
    private float _currentDirX = 1;
    private Rigidbody2D rb;

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

    protected override void EnemyKnockback()
    {
        
    }

    private void CheckWall()
    {
        Vector2 _currentDir = new Vector2(_currentDirX, 0f); 
        _hitWall = Physics2D.Raycast(transform.position, _currentDir, .8f, wallLayerMask);
        if(_hitWall) _currentDirX *= -1;
    }

    private void OnDrawGizmos()
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
