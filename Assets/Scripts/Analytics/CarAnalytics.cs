using UnityEngine;

public class CarAnalytics : MonoBehaviour {

    [SerializeField]
    private float engine;
    private Car car;
    private float min;
    private float max;

    void Start()
    {

          car = GetComponent<Car>();
    }

    public float Velocity
    {
        get { return car.Velocity; }
    }

    public float CO2
    {
        get
        {
            min = engine * 0.2f;
            max = engine * 5;
            if (car.Velocity == 0) return engine * 0.2f;
            return Mathf.Lerp(max, min, car.Velocity / car.MaxSpeed);
        }
    }


    void Update () {
		
	}
}
