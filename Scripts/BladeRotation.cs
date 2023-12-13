using UnityEngine;

public class BladeRotation : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up;
    public float baseRotationSpeed = 10f;
    public float windspeed = 1.0f;
    // WeatherItem resultData;

    [HideInInspector] public float rotationSpeed; // Exposed for UI display
    private float windSpeedMultiplier = 1f; // Multiplier for wind speed effect

    private void OnEnable()
    {
        FetchWeatherData.OnWeatherUpdate += HandleDataUpdate;
    }

    private void OnDisable()
    {
        FetchWeatherData.OnWeatherUpdate -= HandleDataUpdate;
    }

    private void HandleDataUpdate(FetchWeatherData.WeatherItem item)
    {
        // React to the new data here
        
        windspeed = item.wind.speed;
        Debug.Log("Wind Speed Data Changed Unity");
        // Perform actions or update based on the new data
    }
    void Update()
    {
        rotationSpeed = baseRotationSpeed * windspeed* windSpeedMultiplier;
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
        // transform.rotation = Quaternion.Euler(0, 0, windDegree);
    }
    public void SetWindSpeedMultiplier(float multiplier)
    {
        windSpeedMultiplier = Mathf.Clamp(multiplier, 0.1f, 150f); // Adjust the range as needed
    }

}





