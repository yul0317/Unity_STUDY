using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string userName { get; private set; }
    public int userPoint { get; private set; }
    public string topRangkingName { get; private set; } = "Null";
    public int topRangkingPoint { get; private set; } = 0;
    private Data myData = new Data();

    public void UserUpdate(string name)
    {
        if (!myData.UserNameisFind(name))   //! 유저의 정보가 없으면 유저를 추가함. (name이 딕셔너리에 없으면 false)
        {
            userName = name;
            userPoint = 0;
        }
        else    //! 유저의 정보가 있으면 불러옴. (있으면 awake에서 추가를 해놨을꺼임.)
        {
            userName = name;
            userPoint = myData.myData_Dic[name];
        }

    }
    public void DataUpdate(int point)
    {
        if (userName != null)
        {
            if (point < userPoint)  //! 기존에 있던 점수보다 작을 경우에만 점수를 저장.
            {
                myData.ConstructDic(userName, userPoint);
            }
            else
            {
                myData.ConstructDic(userName, point);
            }
        }
    }

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

        LoadData();

    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(myData);                     //SaveData 인스턴스를 JSON 으로 변환
        Debug.Log(json);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        // Application.persistentDataPath  - Unity 메서드를 사용하여 애플리케이션 재설치 또는 업데이트 사이에 유지되는 데이터를 저장할 수 있는 폴더를 반환

        LoadData();
    }

    private void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))                      // .json 파일이 있는지 확인
        {
            Debug.Log(path);
            string json = File.ReadAllText(path);   //내용 읽기
            myData = JsonUtility.FromJson<Data>(json);   //JSON 에서 SaveData 인스턴스로 변환
            topRangkingName = myData.myData_Dic.First().Key;
            topRangkingPoint = myData.myData_Dic.First().Value;
        }
    }

    public void DeleteData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))                      // .json 파일이 있는지 확인
        {
            File.Delete(path);
            myData.myData_Dic.Clear();
        }

    }

    public Dictionary<string, int> ShowDataRangking()
    {
        return myData.myData_Dic;
    }

}
