using UnityEngine;

public class RampSpeedBoost : MonoBehaviour
{
    public float speedBoost = 5f;  
    public float boostDuration = 2f; 
    private float originalSpeed;     
    private bool isOnRamp = false;
    private float boostEndTime;
    private PlayerController playerMovement; 

    void Start()
    {
        playerMovement = GetComponent<PlayerController>();
        if (playerMovement != null)
        {
            originalSpeed = playerMovement.moveSpeed;
        }
    }

    void Update()
    {
        if (isOnRamp && Time.time > boostEndTime)
        {
            playerMovement.moveSpeed = originalSpeed;
            isOnRamp = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ramp"))
        {
            playerMovement.moveSpeed += speedBoost;
            isOnRamp = true;
            boostEndTime = Time.time + boostDuration;
        }
    }
}