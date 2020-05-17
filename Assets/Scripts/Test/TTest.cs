//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-30 10:28:31
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class TTest : MonoBehaviour 
{
    public void ConverTypeToT<T>(List<TTest> tList) where T : TTest,new()
    {
        List<T> TempList = new List<T>();
        for (int i = 0; i < tList.Count; i++)
        {
            T t = new T();
            t = (T)tList[i];
            tList.Add(t);
        }
    }

    
}

public class ATest : TTest
{
    TTest t = new TTest();
    List<TTest> list = new List<TTest>();
    public ATest()
    {
        
    }
    public List<TTest> fun()
    {
        list.Add(new ATest());
        Debug.Log(list[0]);
        return list;
    }

    public void fun1()
    {
        List<ATest> listA = new List<ATest>();

    }
}

public class F1{
    public List<TTest> objList;

}

public class A1
{
    F1 f1 = new F1();
    TTest a = new ATest();
    public List<TTest> fun1()
    {
        f1.objList.Add(a);
        return f1.objList;
    }
}

public class MainTest{
    A1 a = new A1();
    public void Main()
    {
        List<TTest> ttest = new List<TTest>();
        List<ATest> atest = new List<ATest>();
        ttest=a.fun1();
    }
}