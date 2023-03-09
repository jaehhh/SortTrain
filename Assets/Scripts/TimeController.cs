using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private ScoreController scoreController;

    private int time;
    private float currentTime;
    private int min, sec;

    [SerializeField] private int setTime;

    private void Awake()
    {
        min = setTime / 60;
        sec = setTime % 60;

        StartCoroutine("Timer");
    }

    private IEnumerator Timer()
    {
        while(true)
        {
            sec--;

            if (sec < 0)
            {
                sec = 59;
                min--;
            }

            // 시간 표기 및 자리수맞춤
            if (sec < 10)
                timeText.text = $"{min}:0{sec}";
            else
                timeText.text = $"{min}:{sec}";

            yield return new WaitForSeconds(1f);

            // 타임오버 체크
            if (sec == 0 && min == 0)
            {
                scoreController.GameOver();

                break;
            }
        }
    }
}
