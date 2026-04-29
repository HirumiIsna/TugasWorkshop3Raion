using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent<int> onHealthChange;
    public int maxHealth;
    private int _currentHealth;

    void Start()
    {
        _currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
        
        if(gameObject.CompareTag("Player")) onHealthChange.Invoke(_currentHealth); 
        if (amount < 0) DamageTaken();
        if (_currentHealth <= 0) Death();
    }

    private void DamageTaken()
    {
        Debug.Log(gameObject.name + " current health: " + _currentHealth);
    }

    private void Death() //sementara ntar ku pindah lagi
    {
        Debug.Log(gameObject.name + " dead");
        Destroy(gameObject, 0.1f);
    }
}
