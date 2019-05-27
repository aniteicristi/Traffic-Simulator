using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour {

    private Transform cameraControl; // the actual camera
    private Transform cameraRotationPivot; // the rotation pivot of the camera
    private Transform cameraMovementPivot; // the movement pivot of the camera
    private Vector3 localRotation;
    private Vector3 localPosition;
    private Vector3 forwardMovementDirection;
    private Vector3 rightMovementDirection;
    private Vector3 upwardMovementDirection;

    private bool buildingMode = false;

    private float lastZoomAmount;
    private Quaternion lastRotation;

    [SerializeField]
    private float scrollSensitivity = 3f; // the sensitivity of the scroolwheel.
    [SerializeField]
    public float MouseSensitivity = 4f; // the sensitivity of the mouse. (might get changed when we let the user customize the work enviroment himself.
    [SerializeField]
    private float minimumCloseness = -3; // the closest the camera can get to the pivot.
    [SerializeField]
    private float maximumCloseness = -80; // the farthest the camera can go away from the pivot.
    [SerializeField]
    private float orbitDampening = 10f; //camera orbiting speed dampener.
    [SerializeField]
    private float movementSpeedRatio = 1f; // camera horizontal and vertical movement speed.
    [SerializeField]
    private float lowestMapPoint = 0f; // the lowest on the map the camera can go to avoid going under the scene.

	void Start ()
    {
        cameraControl = transform;
        cameraRotationPivot = transform.parent;
        cameraMovementPivot = cameraRotationPivot.transform.parent;
        forwardMovementDirection = new Vector3(cameraMovementPivot.transform.forward.x, cameraMovementPivot.transform.forward.y, cameraMovementPivot.transform.forward.z);
        rightMovementDirection = new Vector3(cameraMovementPivot.transform.right.x, cameraMovementPivot.transform.right.y, cameraMovementPivot.transform.right.z);
	}
	
	void Update ()
    {
		if(Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float scrollAmount = -Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
            scrollAmount *= (cameraControl.transform.localPosition.z * 0.3f);
            scrollAmount += cameraControl.transform.localPosition.z;
            if (scrollAmount <= minimumCloseness && scrollAmount >= maximumCloseness)
                cameraControl.transform.localPosition = new Vector3(cameraControl.transform.localPosition.x, cameraControl.transform.localPosition.y, scrollAmount);
        }

        if(Input.GetMouseButton(1) && !buildingMode)
        {
            if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                localRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                localRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

                cameraMovementPivot.rotation = Quaternion.Euler(0, localRotation.x, 0);

                if (localRotation.y < 0f)
                    localRotation.y = 0f;
                else if (localRotation.y > 90f)
                    localRotation.y = 90f;

                Quaternion QT = Quaternion.Euler(localRotation.y, localRotation.x, 0);
                cameraRotationPivot.rotation = Quaternion.Lerp(cameraRotationPivot.rotation, QT, orbitDampening);
            }
        }

        if(Input.GetKey(KeyCode.D))
            cameraMovementPivot.Translate(rightMovementDirection * movementSpeedRatio);

        if (Input.GetKey(KeyCode.A))
            cameraMovementPivot.Translate(rightMovementDirection * -1 *  movementSpeedRatio);

        if (Input.GetKey(KeyCode.W))
            cameraMovementPivot.Translate(forwardMovementDirection * movementSpeedRatio);

        if(Input.GetKey(KeyCode.S))
            cameraMovementPivot.Translate(forwardMovementDirection * -1 * movementSpeedRatio);

        if (Input.GetKey(KeyCode.Q) && !buildingMode)
            cameraMovementPivot.Translate(cameraMovementPivot.up * -1 * movementSpeedRatio);

        if (Input.GetKey(KeyCode.E) && !buildingMode)
            cameraMovementPivot.Translate(cameraMovementPivot.up * movementSpeedRatio);



        if (cameraMovementPivot.position.y < 0)
            cameraMovementPivot.position = new Vector3(cameraRotationPivot.position.x, lowestMapPoint, cameraRotationPivot.position.z);
    }

    public void SwitchMode()
    {
        if(buildingMode == false)
        {
            lastRotation = cameraRotationPivot.localRotation;
            lastZoomAmount = cameraControl.localPosition.z;
            cameraControl.localPosition = new Vector3(0, 0, maximumCloseness);
            cameraRotationPivot.localRotation = Quaternion.Euler(90, 0, 0);
            cameraMovementPivot.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            cameraRotationPivot.localRotation = lastRotation;
            cameraControl.localPosition = new Vector3(0, 0, lastZoomAmount);
        }
        buildingMode = !buildingMode;
    }
}
