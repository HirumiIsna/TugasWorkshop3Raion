using UnityEngine;
using UnityEngine.UI;

public class UI_Helper : MonoBehaviour
{
    public Image silkBar;
    public Image[] lives;
    public Sprite fullLive;
    public Sprite emptyLive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        silkBar.fillAmount = 0f;

        // if(PlayerPrefs.HasKey("PlayerHealth")) ChangeHealthUI(PlayerPrefs.GetInt("PlayerHealth"));
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

    public void ChangeSilkUI(float _currentSilk, float maxSilk)
    {
        silkBar.fillAmount = _currentSilk / maxSilk;
    }
}
