using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform player;        // Drag your Player here

    [Header("Camera Settings")]
    public float distance = 5.0f;   // Distance behind the player
    public float height = 2.5f;     // Height above the player
    public float smoothSpeed = 10f; // Damping speed

    void Start()
    {
        // SAFETY: If you forgot to assign the player, try to find it automatically
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindWithTag("Player");
            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
            }
            else
            {
                Debug.LogError("CAMERA ERROR: No Player found! Make sure your Player is tagged 'Player'.");
            }
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        // 1. Calculate Target Position
        // Position behind the player's back (using player.forward)
        Vector3 targetPosition = player.position + Vector3.up * height - (player.forward * distance);

        // 2. Smoothly Move
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // 3. Look at Player
        // Look slightly above the player's center (at the head)
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}
