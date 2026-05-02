using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDealDamage
{
    public int onHitDamage;

    void OnTriggerStay2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            Health health = other.GetComponent<Health>();
            DealDamage(onHitDamage * -1, health);   
            Debug.Log(gameObject.name + " hit the player!");
        }
    }

    public void DealDamage(int damage, Health health)
    {
        health.ChangeHealth(damage);
    }

    protected virtual void EnemyKnockback(){}

    protected virtual void MovementLogic(){}
}
