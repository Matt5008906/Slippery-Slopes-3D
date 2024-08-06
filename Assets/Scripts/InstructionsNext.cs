using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionsNext : MonoBehaviour
{
    public Button nextButton; 

    void Start()
    {
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(GoToLevel1);
        }
    }

    void GoToLevel1()
    {
        SceneManager.LoadScene("Lvl1");
    }
}
