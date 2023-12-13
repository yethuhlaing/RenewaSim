using UnityEngine;

public class RotationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float randomAngleX = Random.Range(0.0f, 360.0f);
        Debug.Log(transform.rotation);
        // Get the current rotation of the GameObject
        Quaternion currentRotation = transform.rotation;

        // Create a new rotation only affecting the X-axis
        Quaternion randomXRotation = Quaternion.Euler(randomAngleX, currentRotation.eulerAngles.y, currentRotation.eulerAngles.z);

        // Apply the new rotation
        transform.rotation = randomXRotation;
    }
}
