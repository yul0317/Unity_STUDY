using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class MenuManager : MonoBehaviour
{
    public Text bestScoreText;
    public InputField userName;
    public Button startButton;
    public Button quitButton;
    public Button rangkingButton;
    public Button deleteButton;
    public GameObject scrollView;
    public Text[] rangkingText;


    private void Awake()
    {
        scrollView.SetActive(false);
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(EndGame);
        rangkingButton.onClick.AddListener(ShowRangking);
        deleteButton.onClick.AddListener(DeleteDic);
        foreach (var x in rangkingText)
        {
            x.text = "";
        }
    }

    private void Start()
    {
        bestScoreText.text = "Best Score - " + GameManager.Instance.topRangkingName + " : " + GameManager.Instance.topRangkingPoint;
    }
    void StartGame()
    {
        if (userName != null)
        {
            GameManager.Instance.UserUpdate(userName.text);
            SceneManager.LoadScene(1);
        }
    }

    void EndGame()
    {
        //! 조건부 컴파일..조건부 컴파일을 사용하여 코드가 컴파일된 위치(편집기 또는 빌드용)에 따라 코드를 분기
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif

    }


    void ShowRangking()
    {
        if (!scrollView.activeSelf)
        {
            scrollView.SetActive(true);
            var x = GameManager.Instance.ShowDataRangking();
            int i = 0;
            foreach (var target in x)
            {
                rangkingText[i].text = (i + 1) + "위 - " + target.Key + " : " + target.Value + "점";
                i++;
                if (i == 10) break;
            }
        }
        else
        {
            scrollView.SetActive(false);
        }

    }

    void DeleteDic()
    {
        GameManager.Instance.DeleteData();
    }
}
