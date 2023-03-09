using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private TrainController train;
    [SerializeField] private PassengerSpawner spawner;
    [SerializeField] private SeatState[] seats;

    private void Start()
    {
        for (int i = 0; i < seats.Length; ++i)
        {
            seats[i].Setup();
        }

        spawner.Spawn();

        train.MoveToNext();
    }
}
