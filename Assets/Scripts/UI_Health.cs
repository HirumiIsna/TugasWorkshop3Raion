using UnityEngine;
using UnityEngine.UI;

public class UI_Health : MonoBehaviour
{
    public Image[] lives;
    public Sprite fullLive;
    public Sprite emptyLive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Image live in lives)
        {
           live.sprite = fullLive; 
        }
    }

    public void ChangeHealthUI(int _currentHealth)
    {
        for (int i = 0; i < lives.Length; i++)
        {
            if (i < _currentHealth)
                lives[i].sprite = fullLive;
            else
                lives[i].sprite = emptyLive;
        }
    }
}
