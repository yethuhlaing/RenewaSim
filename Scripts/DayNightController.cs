using UnityEngine;
public class DayNightController : MonoBehaviour
{
    public Light sunLight;
    int currentHour;
    public int dayStartHour = 6; // Example: Day starts at 6 AM
    public int nightStartHour = 18; // Example: Night starts at 6 PM
    void Start(){
        currentHour = GenerateRandomNumber(0,24);
        CheckTimeAndChangeScene(currentHour);
    }
    public int GenerateRandomNumber(int min, int max)
    {
        System.Random random = new System.Random();
        return random.Next(min, max);
    }
    private void CheckTimeAndChangeScene(int currentHour)
    {
        // Compare current time with the desired day and night start times
        if (currentHour >= dayStartHour && currentHour < nightStartHour) // Daytime
        {
            sunLight.intensity = 0.7f; // Full intensity for daytime
            Debug.Log("It's daytime now!");
            // Change other aspects of the scene for daytime if needed
        }
        else // Nighttime
        {
            sunLight.intensity = 0.5f; // Reduced intensity for nighttime
            Debug.Log("It's nighttime now!");
            // Change other aspects of the scene for nighttime if needed
        }
    }
}
