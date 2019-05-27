using UnityEngine;

[System.Serializable]
public class EntrancesArray
{
    public GameObject[] Entrances;
    public float switchTimer;
    public float cooldown;

    [SerializeField]
    private bool isHidden;

    public void ToggleHidden()
    {
        isHidden = !isHidden;
        foreach (GameObject e in Entrances)
        {
            e.SetActive(isHidden);
        }
    }
}

[System.Serializable]
public class EntranceGroup
{
    public EntrancesArray[] entrance;
    private int currentGroup = 0;

    public void SetTimers()
    {
        foreach (EntrancesArray i in entrance)
        {
            i.cooldown = i.switchTimer;
        }
    }

    public void UpdateTimers()
    {
        foreach (EntrancesArray i in entrance)
        {
            i.cooldown -= Time.deltaTime;
            if (i.cooldown <= 0)
            {
                i.ToggleHidden();
                i.cooldown = i.switchTimer;
            }
        }
    }
}

public class TrafficLights : MonoBehaviour
{

    [SerializeField]
    private EntranceGroup entrances;

    bool isHidden = false;

    void Start()
    {
        entrances.SetTimers();
    }

    void Update()
    {
        entrances.UpdateTimers();
    }

    public EntrancesArray[] GetEntrances()
    {
        return entrances.entrance;
    }
}