using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject mainPanel;

    private SoundController soundController;

    private void Awake()
    {
        soundController = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundController>();
    }

    // 메인화면 정지버튼
    public void PauseButton(GameObject popupPanel)
    {
        soundController.ButtonSound();

        Time.timeScale = 0;
        player.canControll = false;

        popupPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    // 정지버튼 해체
    public void ClosePanel(GameObject popupPanel)
    {
        soundController.ButtonSound();

        Time.timeScale = 1;
        player.canControll = true;

        mainPanel.SetActive(true);
        popupPanel.SetActive(false);
    }

    // 팝업
    public void PopupPanel(GameObject popupPanel)
    {
        soundController.ButtonSound();

        popupPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    // 닫기
    public void ClosePanel2(GameObject popupPanel)
    {
        soundController.ButtonSound();

        popupPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    // 라운드클리어-다음라운드 혹은 게임오버-재시작
    public void ReStartScene(bool isClear)
    {
        soundController.ButtonSound();

        if (isClear == false)
        {
            PlayerPrefs.SetInt("Round", 0);
        }

        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 시작화면>메인화면
    public void NextScene(string str)
    {
        soundController.ButtonSound();

        SceneManager.LoadScene(str);
    }

    // 나가기 버튼
    public void QuitEvent()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
