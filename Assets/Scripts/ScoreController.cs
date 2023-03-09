using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    // 기차가 들어올 때마다 바뀌는 조건
    private Condition condition;
    // 조건의 색상 저장
    private Hat hatCondition;
    private Top topCondition;
    private Pants pantsCondition;

    // 조건 : 죽여야 하는가
    private ToKill toKill;
    // 조건에 해당하도록 제어했는지 카운팅
    private int goodCount;

    // 한 라운드에 통과시켜야 하는 기차 수
    [SerializeField] private int maxPass;
    private int currentPass = 0;

    // 라운드 클리어 패널
    [SerializeField] private GameObject panel_Clear;
    [SerializeField] private TextMeshProUGUI text_Round;
    private int round;
    public int Round { get; set; }

    // 게임오버 패널
    [SerializeField] private GameObject panel_GameOver;
    [SerializeField] private TextMeshProUGUI text_GameOverRound;

    // 화면 UI
    [SerializeField] private TextMeshProUGUI text_Condition; // 조건 출력
    [SerializeField] private TextMeshProUGUI text_PassCount; // 진행도
    [SerializeField] private GameObject panel_UI; // 일시정지 버튼

    // 플레이어
    [SerializeField] private PlayerController player;

    // 좌석 개수를 정하는 조건
    public int LevelUpAtRound;

    [SerializeField] private PassengerSpawner spawner;

    public void Awake()
    {
        SetCondition();

        text_PassCount.text = $"{currentPass}/{maxPass}";
    }

    // 기차가 들어올 때마다 조건 세팅
    public void SetCondition()
    {
        // 조건 선택
        toKill = (ToKill)Random.Range(0, (int)ToKill.toKillCount);

        // 조건이 죽여야 할 때
        if (toKill == ToKill.kill)
        {
            // text_Condition.text = "<color=red>kill</color>";
            text_Condition.text = "kill\n";
        }
        // 조건이 죽이지 말아야 할 때
        else
        {
            // text_Condition.text = "<color=green>leave only</color>";
            text_Condition.text = "Leave only\n";
        }

        // 조건 선택
        condition = (Condition)Random.Range(0, (int)Condition.conditionCount);

        // 조건이 모자일 때
        if (condition == Condition.hat)
        {
            hatCondition = (Hat)Random.Range(0, (int)Hat.hatCount);

            if(hatCondition == Hat.none)
            {
                text_Condition.text += "no hat";
            }
            else
            {
                text_Condition.text += "hat";
            }
        }
        // 조건이 상의일 때
        else if (condition == Condition.top)
        {
            topCondition = (Top)Random.Range(0, (int)Top.topCount);

            /*
            string conditionStr = condition.ToString();
            */
            string color = topCondition.ToString();

            /* 색상
            if (color == "brown")
            {
                text_Condition.text += $" <color=#964B00>{color} shirts";
            }
            else
            {
                text_Condition.text += $" <color={color}>{color} shirts";
            }      */

            text_Condition.text += $"{color} shirts";
        }

        // 조건이 하의일 때
        else if(condition == Condition.pants)
        {
            pantsCondition = (Pants)Random.Range(0, (int)Pants.pantsCount);

            /*
            string conditionStr = condition.ToString();
            */
            string color = pantsCondition.ToString();

            /*
            if (color == "brown")
            {
                text_Condition.text += $" <color=#964B00>{color} shirts";
            }
            else
            {
                text_Condition.text += $" <color={color}>{color} shirts";
            }
            */

            text_Condition.text += $"{color} pants";
        }
    }

    // 잘 죽이면 카운팅
    public void GetKillScore(Hat hat, Top top, Pants pants)
    {
        // 죽여야 하는 색상 죽이면 잘함
        if (toKill == ToKill.kill)
        {
            if (condition == Condition.hat && hatCondition == hat)
            {
                goodCount++;
            }
            else if (condition == Condition.top && topCondition == top)
            {
                goodCount++;
            }
            else if (condition == Condition.pants && pantsCondition == pants)
            {
                goodCount++;
            }
        }
        // 특정 색 빼고 모두 죽여야 하니까 색이 다른 승객 죽이면 잘함
        else if(toKill == ToKill.nokill)
        {
            if (condition == Condition.hat && hatCondition != hat)
            {
                goodCount++;
            }
            else if (condition == Condition.top && topCondition != top)
            {
                goodCount++;
            }
            else if (condition == Condition.pants && pantsCondition != pants)
            {
                goodCount++;
            }
        }

        Debug.Log($"goodCount{goodCount}");
    }

    // 잘 살려 보내면 카운팅
   public void GetPassScore(Hat hat, Top top, Pants pants)
    {
        // 특정 색을 죽이지 말아야 할 때 색이 같은 승객 살리면 잘함
        if(toKill == ToKill.nokill)
        {
            if (condition == Condition.hat && hatCondition == hat)
            {
                goodCount++;
            }
            else if (condition == Condition.top && topCondition == top)
            {
                goodCount++;
            }
            else if (condition == Condition.pants && pantsCondition == pants)
            {
                goodCount++;
            }
        }
        // 특정 색을 죽여야 했을 때 색이 다른 승객 살리면 잘함
        else if(toKill == ToKill.kill)
        {
            if (condition == Condition.hat && hatCondition != hat)
            {
                goodCount++;
            }
            else if (condition == Condition.top && topCondition != top)
            {
                goodCount++;
            }
            else if (condition == Condition.pants && pantsCondition != pants)
            {
                goodCount++;
            }
        }
    }

    // 조건 만족에 따라 다음 기차 들여보낼지 혹은 게임오버될지 정함
    public void Scoring()
    {
        int needCount = 0;

        if(PlayerPrefs.GetInt("Round") < LevelUpAtRound)
        {
            needCount = 6;
        }
        else
        {
            needCount = 12;
        }

        if(goodCount == needCount)
        {
            Debug.Log($"goodCount {goodCount} needCount {needCount}");

            currentPass++;
            goodCount = 0;

            text_PassCount.text = $"{currentPass}/{maxPass}";

            if(currentPass == maxPass)
            {
                RoundClear();

                return;
            }

            SetCondition();

            spawner.Spawn();
        }
        else
        {
            Debug.Log($"goodCount {goodCount} needCount {needCount}");

            GameOver();
        }
    }

    private void RoundClear()
    {
        Round = PlayerPrefs.GetInt("Round");
        PlayerPrefs.SetInt("Round", ++Round);

        panel_Clear.SetActive(true);
        text_Round.text = $"Clear. R{Round}";

        panel_UI.SetActive(false);

        Time.timeScale = 0;
        player.canControll = false;
    }

    public void GameOver()
    {
        Round = PlayerPrefs.GetInt("Round");

        panel_GameOver.SetActive(true);
        text_GameOverRound.text = $"GameOver. R{Round}";

        panel_UI.SetActive(false);

        Time.timeScale = 0;
        player.canControll = false;
    }
}
