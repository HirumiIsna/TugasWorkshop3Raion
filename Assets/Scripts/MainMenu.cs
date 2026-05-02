using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;

    void OnEnable()
    {
        playButton?.onClick.AddListener(Play);
        quitButton?.onClick.AddListener(Quit);
    }

    void OnDisable()
    {
        playButton?.onClick.RemoveListener(Play);
        quitButton?.onClick.RemoveListener(Quit);
    }

    public void Play()  
    {
        ChangeSceneManager.instance.ChangeScene("Room_1");
    }

    public void Quit()
    {
        if(Application.isEditor) UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
