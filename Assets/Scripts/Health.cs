using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Health e gak tau perlu apa gak")]
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
    }
}
