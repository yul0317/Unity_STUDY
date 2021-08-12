using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class MenuManager : MonoBehaviour
{
    public InputField userName;
    public Button startButton;
    public Button quitButton;

    private void Awake()
    {
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(EndGame);
    }

    void StartGame()
    {
        if (userName != null)
        {
            GameManager.Instance.userName = userName.text;
        }
        SceneManager.LoadScene(1);
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
}
