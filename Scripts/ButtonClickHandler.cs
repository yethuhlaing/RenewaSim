
using UnityEngine;
using UnityEngine.UI;
public class ButtonClickHandler : MonoBehaviour
{  
    public Button switchButton;
    public CameraSwitcher cameraSwitcher;

    void Start()
    {
        switchButton.onClick.AddListener(SwitchCamera);
    }

    void SwitchCamera()
    {
        cameraSwitcher.SwitchCamera();
    }
}
