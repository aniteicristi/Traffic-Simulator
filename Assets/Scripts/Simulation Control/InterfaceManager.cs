using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public bool buildMode;

    [SerializeField]
    CameraControler cameraControl;

    [SerializeField]
    GameObject buildPanel;

    [SerializeField]
    GameObject analyticsPanel;

    [SerializeField]
    RoadCustomizationPanel roadCustomizationPanel;

    Linker[] linkers;

    private void Start()
    {
        cameraControl = FindObjectOfType<CameraControler>();
        linkers = FindObjectsOfType<Linker>();
        foreach (Linker linker in linkers)
        {
            linker.gameObject.SetActive(false);
        }
    }

    public void SpawnRoad(MapStructure road)
    {
        Instantiate(road);
    }

    public void ToggleRoadPanel(Transform road, string tag, bool value)
    {
        if (!buildMode)
            return;
        roadCustomizationPanel.gameObject.SetActive(value);
        if(value)
            roadCustomizationPanel.SelectRoad(road, tag);
    }

    public void ToggleBuildMode()
    {
        if (Time.timeScale > 0)
            return;

        cameraControl.SwitchMode();
        if(buildMode)
        {
            ToggleRoadPanel(transform, "interface", false);
            buildPanel.SetActive(false);
        }
        else
        {
            buildPanel.SetActive(true);
            analyticsPanel.SetActive(false);
        }
        
        buildMode = !buildMode;
        ToggleLinkingInterface(buildMode);

    }
    
    public void ToggleLinkingInterface(bool value)
    {
        if(!value)
        {
            linkers = FindObjectsOfType<Linker>();
        }
        foreach(Linker linker in linkers)
        {
            linker.gameObject.SetActive(value);
        }
    }
    
    public void ToggleAnalytics()
    {
        if (buildMode)
            ToggleBuildMode();
        analyticsPanel.SetActive(!analyticsPanel.activeSelf);
    }
}
