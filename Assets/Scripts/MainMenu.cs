using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton; 

    void Start()
    {
        if (playButton != null)
        {
            playButton.onClick.AddListener(LoadInstructions);
        }
    }

    void LoadInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }
}
