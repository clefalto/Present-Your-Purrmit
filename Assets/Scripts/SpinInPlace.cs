using UnityEngine;

public class SpinInPlace : MonoBehaviour
{
    [Tooltip("Rotation speed in degrees per second")]
    public float rotationSpeed = 20f;

    void Update()
    {
        // Rotate around the Y axis in world space
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}
