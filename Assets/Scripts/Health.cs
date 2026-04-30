using UnityEngine;
using UnityEngine.Events;
using System.Collections;
public class Health : MonoBehaviour
{
    public UnityEvent<int> onHealthChange;
    public int maxHealth;
    private int _currentHealth;
    private SpriteRenderer _sprite;
    private Material _originalMat;
    [SerializeField] private Material _onDamageMat;
    public bool _iFrame = false;
    [SerializeField] private float iFrameTime;

    void Start()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _originalMat = _sprite.material;
        _currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        if(_iFrame) return;
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
        
        if(gameObject.CompareTag("Player")) onHealthChange.Invoke(_currentHealth); 
        if (amount < 0) DamageTaken();
        if (_currentHealth <= 0) Death();
    }

    private void DamageTaken()
    {
        if(gameObject.CompareTag("Player")) StartCoroutine(IFrame()); 
        Debug.Log(gameObject.name + " current health: " + _currentHealth);
        StartCoroutine(DamageFlash());
    }

    private IEnumerator DamageFlash()
    {
        _sprite.material = _onDamageMat;
        yield return new WaitForSeconds(.25f);
        _sprite.material = _originalMat;
    }
    
    private IEnumerator IFrame()
    {
        _iFrame = true;
        yield return new WaitForSeconds(iFrameTime);
        _iFrame = false;
    }

    private void Death() 
    {
        Debug.Log(gameObject.name + " dead");
        Destroy(gameObject, 0.1f);
    }
}
