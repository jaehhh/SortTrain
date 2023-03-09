using UnityEngine;

public class SeatState : MonoBehaviour
{
    public bool IsEmpty { get; set; }

    public void Setup()
    {
        IsEmpty = true;
    }

    public void Shot()
    {
        if (IsEmpty)
        {
            Debug.Log("seatState IsEmpty");
            return;
        }

        else
        {
            GetComponentInChildren<PassengerController>().Shot();

            IsEmpty = true;
        }
    }
}
