using UnityEngine;
using UnityEngine.UI;

public class StopCar : MonoBehaviour
{
    [SerializeField]
    private Text buttontext;

    private Car car;

    /*[SerializeField]
     private Text simulationtext; */

    private Rigidbody body;
    public bool IsCarStopped = false;


    private void Start()
    {
        body = GetComponent<Rigidbody>();
        car = GetComponent<Car>();
    }


    public void Button_Click()
    {
       
            if (IsCarStopped)
            {
                buttontext.text = "Stop the car";
                car.enabled = true;
                IsCarStopped = false;

            }
            else
            {
                buttontext.text = "Start the car";
                body.velocity = Vector3.zero;
                IsCarStopped = true;
                car.enabled = false;
            }
        
    }
}


