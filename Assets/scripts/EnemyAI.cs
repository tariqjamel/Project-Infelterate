using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public Transform player;        
    public GameManager gameManager; 
    public Transform[] patrolPoints; 

    [Header("Settings")]
    public float catchDistance = 1.5f; 
    public float chaseSpeedMultiplier = 2.0f; 
    public float waitTimeAtPoint = 2.0f; 

    private NavMeshAgent agent;
    private Animator animator; 
    private int currentPointIndex = 0;
    private bool isChasing = false;
    private bool isWaiting = false;
    private float waitTimer = 0f;
    private float defaultSpeed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); 
        defaultSpeed = agent.speed; // Remember walking speed

        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[0].position);
        }
    }

    void Update()
    {
        // 1. UPDATE ANIMATIONS
        UpdateAnimations();

        if (player == null) return;

        
        if (isChasing)
        {
            agent.SetDestination(player.position);
            
            // --- CHANGED HERE: DETECT CATCH ---
            // If the enemy is close enough to touch the player
            if (Vector3.Distance(transform.position, player.position) < catchDistance)
            {
                // Find the MenuManager and load the "Failed" scene
                // (Make sure you have the MenuManager script in the scene!)
// Load the scene directly (Requires "using UnityEngine.SceneManagement;" at the top)
UnityEngine.SceneManagement.SceneManager.LoadScene("FailedScene");
            }
        }
       
        else
        {
            if (isWaiting)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer >= waitTimeAtPoint)
                {
                    isWaiting = false;
                    GoToNextPoint();
                }
                return; 
            }

            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                isWaiting = true;
                waitTimer = 0f;
            }
        }
    }

    // Controls the parameters based on speed
    void UpdateAnimations()
    {
        if (animator == null) return;

        // Get the current speed of the NavMesh Agent
        float speed = agent.velocity.magnitude;

        // Rule 1: If moving at all, set isWalking
        bool walking = speed > 0.1f;
        
        // Rule 2: If moving faster than default speed, set isRunning
        bool running = speed > defaultSpeed + 0.5f;

        animator.SetBool("isWalking", walking);
        animator.SetBool("isRunning", running);
        
        // Ensure "isCrouching" is off for the enemy
        animator.SetBool("isCrouching", false);
    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 0) return;
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        agent.SetDestination(patrolPoints[currentPointIndex].position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true;
            isWaiting = false;
            agent.speed = defaultSpeed * chaseSpeedMultiplier; 
        }
    }
}
