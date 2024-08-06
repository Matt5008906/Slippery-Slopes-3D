using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetection : MonoBehaviour
{
    public AudioClip deathSound; 
    private AudioSource audioSource; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player collided with an obstacle
        if (other.CompareTag("Obstacle"))
        {
            PlayDeathSound(); 
            RestartGame();    
        }
    }

    void PlayDeathSound()
    {
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
