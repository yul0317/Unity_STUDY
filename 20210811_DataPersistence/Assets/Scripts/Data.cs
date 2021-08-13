using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
class Data : ISerializationCallbackReceiver
{
    // public string userName;
    // public int userPoint;
    public List<string> userNameList;   //! 유저 이름 담을 리스트 (저장용)
    public List<int> userPointList;     //! 유저 점수 담을 리스트 (저장용)

    [NonSerialized]
    public Dictionary<string, int> myData_Dic = new Dictionary<string, int>();   //딕셔너리 활용 (씬 관리용)

    public void ConstructDic(string name, int point)
    {
        if (myData_Dic.ContainsKey(name))
        {
            myData_Dic[name] = point;
        }
        else
        {
            myData_Dic.Add(name, point);
        }
        myData_Dic = myData_Dic.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        //! 딕셔너리를.. OrderBy(확장메서드) 하며 람다식을 통하여 IEnumerable 모르겠다...
        //! 누가 속시원히 설명좀...}

    }
    public Dictionary<string, int> Show_myData_Dic()
    {
        return myData_Dic;
    }

    public bool UserNameisFind(string name)
    {
        return myData_Dic.ContainsKey(name);
    }

    public void OnBeforeSerialize()     //! 직렬화를 하기전에 불러오는 메소드. 딕셔너리를 리스트에 추가해줌.
    {
        userNameList = new List<string>(myData_Dic.Keys);
        userPointList = new List<int>(myData_Dic.Values);
    }

    public void OnAfterDeserialize()    //! 역직렬화를 한 후에 불러오는 메소드. 딕셔너리에 추가를 해줌.
    {
        //data = new Dictionary<string, int>(); //? 이부분 왜있을까

        for (int i = 0; i < Math.Min(userNameList.Count, userPointList.Count); i++)
        //Math.Min - 두 수중 작은값 반환
        {
            myData_Dic.Add(userNameList[i], userPointList[i]);
        }
    }
}
