using DigitalRuby.RainMaker;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // Assign your cameras in the Inspector
    private int currentCameraIndex = 0;
    public RainScript rainScript;

    void Start()
    {
        rainScript = GameObject.FindWithTag("RainPrefab").GetComponent<RainScript>();
        rainScript.Camera = cameras[0];
        // Disable all cameras except the first one
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
    }

    public void SwitchCamera()
    {
        cameras[currentCameraIndex].gameObject.SetActive(false);

        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        cameras[currentCameraIndex].gameObject.SetActive(true);

        rainScript.Camera = cameras[currentCameraIndex];
    }
}
