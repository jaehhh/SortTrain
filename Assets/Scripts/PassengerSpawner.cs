using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] passengerPrefab;
    [SerializeField] private GameObject[] seats;

    [SerializeField] private ScoreController scoreController;

    public int Mans { get; set; }

    public void Spawn()
    {
        int spawnLength = 0;

        if (PlayerPrefs.GetInt("Round") < scoreController.LevelUpAtRound)
        {
            spawnLength = seats.Length / 2;
        }
        else
        {
            spawnLength = seats.Length;
        }

        while( Mans < spawnLength)
        {
            int random = Random.Range(0, passengerPrefab.Length);

            GameObject clone = Instantiate(passengerPrefab[random], seats[Mans].transform.position, Quaternion.identity);

            clone.transform.SetParent(seats[Mans].transform);

            seats[Mans].GetComponent<SeatState>().IsEmpty = false;
            Mans++;
        }
    }

    public void RemovePassenger()
    {
        for (int i = 0; i < Mans; i++)
        {
           seats[i].GetComponentInChildren<PassengerController>().Remove();
        }

        Mans = 0;   
    }
}