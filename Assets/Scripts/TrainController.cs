using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private PassengerSpawner spawner;
    [SerializeField] private Transform[] pos;

    private int currentIndex = 0;
    private int nextIndex = 1;

    private float trainSpeed;
    private int accelerate = 0; // 감속 : 0, 가속 : 1
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;


    [SerializeField] private Sprite[] sprite;
    [SerializeField] private GameObject[] highLevelKeyGuide;

    // 사운드
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;

    private void Awake()
    {
        SpriteRenderer mySprite = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("Round") < scoreController.LevelUpAtRound)
        {
            mySprite.sprite = sprite[0];
        }
        else
        {
            mySprite.sprite = sprite[1];

            for(int i = 0; i <highLevelKeyGuide.Length; ++i)
            {
                highLevelKeyGuide[i].SetActive(true);
            }
        }
    }

    public void MoveToNext()
    {
        StartCoroutine("Move");
    }

    private IEnumerator Move()
    {
        while (true)
        {
            PlaySound();

            nextIndex = currentIndex + 1;

            // 이동 루틴 실행
            yield return StartCoroutine("Move2");

            // 이동 끝나면
            currentIndex++;

            // 정차 지점이 끝지점이 아니면 while문 탈출
            if (currentIndex != pos.Length - 1)
            {
                accelerate = 1;
                break;
            }
            // 정차 지점이 끝지점
            else
            {  
                Arrive();

                // 또한 조건 달성 여부 판단
                scoreController.Scoring();
            }
        }

        // 플랫폼에 정차하면 키입력 가능하도록 함
        player.canNextTo = true;
    }

    // 기차가 오른쪽으로 가속 이동하는 루틴
    private IEnumerator Move2()
    {
        float percent = 0;

        while (percent <= 0.9999f)
        {
            if(accelerate == 0)
            {
                trainSpeed = Mathf.Lerp(maxSpeed, minSpeed, percent);
            }
            else
            {
                trainSpeed = Mathf.Lerp(minSpeed, maxSpeed, percent);
            }

            percent += Time.deltaTime * trainSpeed;

            transform.position = Vector3.Lerp(pos[currentIndex].position, pos[nextIndex].position, percent);

            yield return null;
        }

        transform.position = pos[nextIndex].position;
    }

    private void Arrive()
    {
        // 남은 승객을 지우면서 점수 처리
        spawner.RemovePassenger();

        currentIndex = 0;

        accelerate = 0;
    }

    private void PlaySound()
    {
        if (currentIndex == 0)
        {
            audioSource.clip = clips[0];
        }
        else
        {
            audioSource.clip = clips[1];
        }
        audioSource.Play();
    }
}
