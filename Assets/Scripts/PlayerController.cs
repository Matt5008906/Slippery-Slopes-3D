using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 15f;            
    public float jumpForce = 15f;           
    public float groundCheckDistance = 0.2f; 

    private Rigidbody rb;
    private Animator playerAnimator;
    private bool isGrounded;
    private bool isMoving;
    private bool hasJumped;
    private Vector3 lastPosition;           

    public AudioClip snowSound; 
    private AudioSource audioSource;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>(); 
        lastPosition = transform.position;   

        audioSource = GetComponent<AudioSource>();

        if (audioSource != null && snowSound != null)
        {
            audioSource.clip = snowSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }



    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, LayerMask.GetMask("Default"));

        isMoving = (transform.position - lastPosition).magnitude > 0.01f;

        playerAnimator.SetBool("IsGrounded", isGrounded);
        playerAnimator.SetBool("IsMoving", isMoving);

        if (isGrounded)
        {
            hasJumped = false;
        }

        if (isGrounded && Input.GetButtonDown("Jump") && !hasJumped)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            hasJumped = true;
        }
        lastPosition = transform.position;

    }

    void FixedUpdate()
    {
        // Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        movement = transform.TransformDirection(movement);

        Vector3 targetVelocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.z * moveSpeed);

        if (movement.z < 0)
        {
            targetVelocity.z = Mathf.Max(targetVelocity.z, 0f);
        }

        rb.velocity = targetVelocity;
    }
}