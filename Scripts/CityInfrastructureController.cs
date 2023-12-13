using UnityEngine;
using UnityEngine.UI;
public class CityInfrastructureController : MonoBehaviour
{
    // Start is called before the first frame update
    public Text GeneralText;
    public void UpdateCityDetails(){
        // Replace "YourTag" with the tag you've assigned to the GameObjects you want to count
        GameObject[] buidlings = GameObject.FindGameObjectsWithTag("Building");
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        GameObject[] solarPanels = GameObject.FindGameObjectsWithTag("Solar Panel");
        GameObject[] windTurbines = GameObject.FindGameObjectsWithTag("Wind Turbine");

        GeneralText.text = "Total buildings': " + buidlings.Length + "\n"+
        "Total Trees': " + trees.Length + "\n"+
        "Total Solar Panels': " + solarPanels.Length + "\n"+
        "Total Wind Turbines': " + windTurbines.Length + "\n";
    }

}
