using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Analytics : MonoBehaviour
{
    [SerializeField]
    private Text averageSpeedUI;

    [SerializeField]
    private Text stillCarsUI;

    [SerializeField]
    private Text stillTimeUI;

    [SerializeField]
    private Text totalTimeUI;

    [SerializeField]
    private Text carbonEmissionsUI;

    private List<CarAnalytics> cars;
    private WaitForSeconds waitForSeconds = new WaitForSeconds(1);

    private Coroutine coroutine;

    float averageSpeed;
    float stillTime;
    float carbonEmissions;

    float totalTime;
    float totalDistanceTraveled;
    float carbonEmissionsTotal;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        cars = FindObjectsOfType<CarAnalytics>().ToList();
        coroutine = StartCoroutine(Watch());
    }

    public void StopWatchCoroutine()
    {
        StopCoroutine(coroutine);
    }

    public void Reset()
    {
        averageSpeed = 0;
        stillTime = 0;
        carbonEmissions = 0;

        totalDistanceTraveled = 0;
        totalTime = 0;
        carbonEmissionsTotal = 0;

        UpdateUI(stoppedCars: 0);
    }

    private IEnumerator Watch()
    {
        while (true)
        {
            yield return waitForSeconds;

            float totalDistanceTraveledFrame = 0;
            float carbonEmissionsFrame = 0;
            int stoppedCars = 0;

            foreach (var car in cars)
            {
                carbonEmissionsFrame += car.CO2;
                totalDistanceTraveledFrame += car.Velocity * 10;

                if (car.Velocity == 0)
                {
                    stoppedCars++;
                    stillTime++;
                }
            }

            carbonEmissionsTotal += carbonEmissionsFrame;
            carbonEmissions = carbonEmissionsTotal / totalTime;

            totalDistanceTraveled += totalDistanceTraveledFrame;
            averageSpeed = totalDistanceTraveled / totalTime;

            UpdateUI(stoppedCars);
        }
    }

    public void AddCar(Car c)
    {
        cars.Add(c.GetComponent<CarAnalytics>());
    }

    public void RemoveCar(Car c)
    {
        cars.Remove(c.GetComponent<CarAnalytics>());
    }

    private void UpdateUI(int stoppedCars)
    {
        carbonEmissionsUI.text = carbonEmissions.ToString("0.## ");
        stillCarsUI.text = stoppedCars.ToString();
        averageSpeedUI.text = averageSpeed.ToString("0.## m/s");
        stillTimeUI.text = stillTime.ToString("0.## s");

        totalTimeUI.text = totalTime.ToString("0 s");
    }

    private void Update()
    {
        totalTime += Time.deltaTime;
    }
}
