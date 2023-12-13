using UnityEngine;
using UnityEngine.UI;

public class SolarPanelsController : MonoBehaviour
{
    public Text GeneralText;
    public int numSolarPanels;
    public float SolarWatt = 300.0f;
    public float solarPanelArea = 1.6f;
    public float solarPanelEfficiency = 0.18f;
    public float sunlightIntensity = 1000.0f;

    void Start(){
        GameObject[] solarPanels = GameObject.FindGameObjectsWithTag("Solar Panel");
        numSolarPanels = solarPanels.Length;
    }

    // Start is called before the first frame update
    public void CalculateGeneratedPower(){
        
        float TotalpowerOutput = solarPanelArea * solarPanelEfficiency * sunlightIntensity * numSolarPanels;

        GeneralText.text = "Generated Power from " + numSolarPanels + " Solar Panels is " + TotalpowerOutput + " watts. " +
        "This is an ideal scenario and actual output might vary based on real-world conditions such as temperature, shading, panel orientation, and inefficiencies in the system";
    }
}
