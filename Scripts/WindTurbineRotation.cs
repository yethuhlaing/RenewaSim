using UnityEngine;

public class WindTurbineRotation : MonoBehaviour
{
    public string bladeNamePrefix = "blade";
    public int numberOfBlades = 3; // Adjust this to match the number of blades
    public float rotationSpeed = 10f;

    void Update()
    {
        // Rotate each blade based on its name
        for (int i = 1; i <= numberOfBlades; i++)
        {
            string bladeName = $"{bladeNamePrefix}{i}";
            Transform blade = transform.Find(bladeName);

            if (blade != null)
            {
                blade.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
            else
            {
                Debug.LogWarning($"Blade '{bladeName}' not found.");
            }
        }
    }
}
