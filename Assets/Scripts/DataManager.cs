using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SaveHealth(int amount)
    {
        PlayerPrefs.SetInt("PlayerHealth", amount);
    }

    public int LoadHealth()
    {
        return PlayerPrefs.GetInt("PlayerHealth");
    }

    public void SaveSilk(float amount)
    {
        PlayerPrefs.SetFloat("PlayerSilk", amount);
    }

    public float LoadSilk()
    {
        return PlayerPrefs.GetFloat("PlayerSilk");
    }
}
