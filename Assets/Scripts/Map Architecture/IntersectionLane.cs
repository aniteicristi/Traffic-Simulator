using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class IntersectionLane : Lane
{
    [SerializeField]
    public BasicLane[] Exits;

    public override Transform End
    {
        get
        {
            return null;
        }
    }

    public override Lane Next
    {
        get
        {
            return null;
        }

        set { }
    }

    public BasicLane GetRandomExit(Transform carPosition)
    {
        BasicLane exit;

        do
        {
            exit = Exits[Random.Range(0, Exits.Length)];
        } while (carPosition.forward + exit.End.transform.forward == Vector3.zero);

        return exit;
    }
}