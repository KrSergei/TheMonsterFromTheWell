using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpotStorage : MonoBehaviour
{
    public Transform[] spots;

    private void Awake()
    {
        spots = GetComponentsInChildren<Transform>();
    }

    public int GetSpotsLength()
    {
        return spots.Length;
    }

    public Transform GetNextMovementSpot(int indexSpot)
    {
        return spots[indexSpot];
    }
}
