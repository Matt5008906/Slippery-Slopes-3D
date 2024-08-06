using UnityEngine;
using UnityEngine.SceneManagement; // For scene management

public class FinishLine : MonoBehaviour
{
    public string nextSceneName; 
    public ParticleSystem finishLineParticles; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnWin();
        }
    }

    private void OnWin()
    {
        if (finishLineParticles != null)
        {
            finishLineParticles.Play();
        }

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Invoke("LoadNextScene", 1f); 
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}