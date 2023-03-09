using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // 기차 보내기
    [SerializeField] private TrainController train;
    [SerializeField] private KeyCode nextToKey;
    public bool canNextTo;

    // 발사할 좌석
    [SerializeField] private GameObject[] seats;

    // 총 발사
    [SerializeField] private KeyCode[] fireKey;
    [SerializeField] private int maxBullet;
    private int currentBullet;

    // 총알 UI
    [SerializeField] private GameObject[] bullet;
    // 섬광
    [SerializeField] private Image screen;
    [SerializeField] private float flashSpeed;

    // 총 장전
    [SerializeField] private KeyCode reloadKey;
    private bool isReload = false;
    [SerializeField] private float reloadTime;
    private float currentTime;

    // 플레이어 조작 가능 여부
    public bool canControll;

    // 사운드
    [SerializeField] private AudioSource shotSound;
    [SerializeField] private AudioSource reloadSound;

    private void Awake()
    {

        canControll = true;
        canNextTo = false;

        currentBullet = maxBullet;
    }

    private void Update()
    {
        if (canControll == false) return;

        // 총 발사
        if (isReload == false)
        {
            for (int i = 0; i < seats.Length; ++i)
            {
                if (Input.GetKeyDown(fireKey[i]))
                {
                    Shot(seats[i]);
                }
            }

            if (Input.GetKeyDown(reloadKey))
            {
                StartCoroutine("Reload");
            }
        }

        // 기차 보내기
        if (Input.GetKeyDown(nextToKey) && canNextTo == true)
        {
            canNextTo = false;

            train.MoveToNext();
        }
    }

    private void Shot(GameObject seat)
    {
        if (currentBullet > 0)
        {
            DecreaseBullet(currentBullet);

            seat.GetComponent<SeatState>().Shot();

            shotSound.Play();

            StopCoroutine("ShotFlash");
            StartCoroutine("ShotFlash");
        }

        else if(isReload == false)
        {
            StartCoroutine("Reload");
        }
    }

    private IEnumerator Reload()
    {
        isReload = true;

        reloadSound.Play();

        while(currentTime <= reloadTime)
        {
            currentTime += Time.deltaTime;

            yield return null;
        }

        currentTime = 0;

        IncreaseBullet();

        isReload = false;
    }

    private void DecreaseBullet(int bullet)
    {
        this.bullet[bullet-1].SetActive(false);

        currentBullet--;
    }

    private void IncreaseBullet()
    {
        for (int i = 0; i < maxBullet; i++)
        {
            bullet[i].SetActive(true);
        }

        currentBullet = maxBullet;
    }

    private IEnumerator ShotFlash()
    {
        Color color = screen.color;

        while(color.a < 0.1)
        {
            color.a += flashSpeed * Time.deltaTime;

            screen.color = color;

            yield return null;
        }

        while(color.a >= 0)
        {
            color.a -= flashSpeed * Time.deltaTime;

            screen.color = color;

            yield return null;
        }
    }
}