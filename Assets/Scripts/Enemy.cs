using UnityEngine;

public class Enemy : MonoBehaviour, IDamageOnHit //jangan pake interface dlu blom tentu penggunaanya
{
    public int onHitDamage;

    void OnTriggerEnter2D(Collider2D other) //ganti ke stay klo dah ada iframenya
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Health>()?.ChangeHealth(-1);
            Debug.Log(gameObject.name + " hit the player!");
        }    
    }
    

    public void DealDamage(int damage){}
}
