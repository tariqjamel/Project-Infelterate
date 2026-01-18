using UnityEngine;

public class Briefcase : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.CollectBriefcase();
            Destroy(gameObject); 
        }
    }
}
