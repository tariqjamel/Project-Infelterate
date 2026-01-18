using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float runSpeed = 9f;
    public float rotationSpeed = 100.0f; // Adjusted for smoother turning
    public float jumpHeight = 2f;
    public float gravity = -9.8f;

    private float currentSpeed;
    private Vector3 velocity; // For gravity and jumping
    private CharacterController controller;
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. INPUT
        float h = Input.GetAxis("Horizontal");  // Turn Left/Right (A/D)
        float v = Input.GetAxis("Vertical");    // Move Forward/Backward (W/S)

        // 2. ROTATION (Turn the character)
        // We rotate the character based on Horizontal input
        transform.Rotate(0, h * rotationSpeed * Time.deltaTime, 0);

        // 3. MOVEMENT (Forward/Backward only)
        // We removed "transform.right * h" so you don't strafe sideways
        Vector3 move = transform.forward * v;
        
        // Check if actually moving for animation
        bool moving = Mathf.Abs(v) > 0.1f; 

        // 4. RUNNING vs WALKING
        if (Input.GetKey(KeyCode.LeftShift) && moving)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
            currentSpeed = runSpeed;
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", moving);
            currentSpeed = walkSpeed;
        }

        // 5. CROUCHING
        bool isCrouching = Input.GetKey(KeyCode.C);
        animator.SetBool("isCrouching", isCrouching);
        if (isCrouching) currentSpeed = walkSpeed * 0.5f;

        // 6. GRAVITY & JUMPING
        if (controller.isGrounded)
        {
            velocity.y = -2f; // Stick to ground
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // 7. APPLY MOVEMENT
        controller.Move(move * currentSpeed * Time.deltaTime); // Move Forward/Back
        controller.Move(velocity * Time.deltaTime);            // Apply Gravity/Jump
    }
}
