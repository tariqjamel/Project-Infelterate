using UnityEngine;

public class Helipad : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.WinGame();
        }
    }
}
