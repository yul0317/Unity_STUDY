using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }
    public Color TeamColor; // new variable declared
    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }

    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;

        string json = JsonUtility.ToJson(data);                     //SaveData 인스턴스를 JSON 으로 변환

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        // Application.persistentDataPath  - Unity 메서드를 사용하여 애플리케이션 재설치 또는 업데이트 사이에 유지되는 데이터를 저장할 수 있는 폴더를 반환
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))                      // .json 파일이 있는지 확인
        {
            string json = File.ReadAllText(path);   //내용 읽기
            SaveData data = JsonUtility.FromJson<SaveData>(json);   //JSON 에서 SaveData 인스턴스로 변환

            TeamColor = data.TeamColor;
        }
    }
}