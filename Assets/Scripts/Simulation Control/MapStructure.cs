using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStructure : MonoBehaviour {

    public InterfaceManager interfaceManager;

    private Vector3 draggingOffset;
    private Vector3 screenPoint;

    [SerializeField]
    private string StructureTag;

    public void Start()
    {
        interfaceManager = FindObjectOfType<InterfaceManager>();
        transform.parent = null;
    }

    public void OnMouseDown()
    {
        interfaceManager.ToggleRoadPanel(transform, StructureTag, true);
        if(interfaceManager.buildMode)
        {
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            draggingOffset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    public void OnMouseDrag()
    {
        if (interfaceManager.buildMode)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + draggingOffset;
            transform.position = curPosition;
        }
    }
}
