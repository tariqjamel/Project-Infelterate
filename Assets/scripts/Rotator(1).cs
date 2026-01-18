using UnityEngine;

public class RotateObject : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.forward * 50f * Time.deltaTime); // Rotate around Y axis
    }
}
