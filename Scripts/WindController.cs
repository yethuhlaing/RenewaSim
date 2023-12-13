using UnityEngine;
public class WindController : MonoBehaviour
{
    public BladeRotation bladeRotation;

    void Update()
    {
        // Simulate changing wind speed over time
        float windSpeed = Mathf.PingPong(Time.time * 0.5f, 5f) + 5f; // Adjust as needed
        bladeRotation.SetWindSpeedMultiplier(windSpeed);
    }
}
