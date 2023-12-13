using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 90f;
    [SerializeField] private float rotationSpeed = 50f;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("CameraController script started");
    }

    // Update is called once per frame
    void Update()
    {
        // Get input for camera movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // Get input for camera rotation


        // Rotate the camera
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isRotatingLeft = true;
        }
        // Stop rotating left when 'Q' key is released
        if (Input.GetKeyUp(KeyCode.Q))
        {
            isRotatingLeft = false;
        }

        // Start rotating right when 'E' key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            isRotatingRight = true;
        }
        // Stop rotating right when 'E' key is released
        if (Input.GetKeyUp(KeyCode.E))
        {
            isRotatingRight = false;
        }

        // Continuous rotation while keys are pressed
        if (isRotatingLeft)
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        if (isRotatingRight)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        // Calculate movement direction
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Move the camera forward and backward
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Check if the Space key is pressed for upward movement
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }

        // Check if the Left Ctrl key is pressed for downward movement
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
    }

}
