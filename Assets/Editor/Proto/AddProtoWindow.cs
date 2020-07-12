//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-17 17:28:16
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/// <summary>
/// 协议脚本自动生成器，
/// </summary>
public class AddProtoWindow : EditorWindow
{
    private GUIStyle mGUIStyle;
    private string mProtoFileName;  //文件名
    private string mProtoFileAnotation; //文件注释
    private int mFieldCount; //字段个数
    private string[] mFieldTypeGroup; //字段类型详情，预设50个
    private string[] mFieldNameGroup;   //字段名称详情，预设50个
    private string[] mFieldAnotationGroup;   //字段名称详情，预设50个
    private bool[] mIsLoopGroup;    //是否循环，预设50个
    private bool[] mBoolGroup;   //是否有if判断，预设50个

    private void OnEnable()
    {
        mFieldCount = 0;
        mFieldTypeGroup = new string[50];
        mFieldNameGroup = new string[50];
        mFieldAnotationGroup = new string[50];
        mIsLoopGroup = new bool[50];
        mGUIStyle = new GUIStyle();
       //bb.normal.background = null;    //这是设置背景填充的
        mGUIStyle.normal.textColor = new Color(1, 1, 1);   //设置字体颜色的
        mGUIStyle.fontSize = 13;       //当然，这是字体颜色
    }

    public AddProtoWindow()
    {

    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("", GUILayout.Width(2));   //空2个单位，不贴边
        GUILayout.Label("协议文件名", mGUIStyle,GUILayout.Width(80));
        this.mProtoFileName = EditorGUILayout.TextField(this.mProtoFileName, GUILayout.MaxWidth(100), GUILayout.MaxHeight(20));
        GUILayout.Label("协议注释", mGUIStyle, GUILayout.Width(60));
        this.mProtoFileAnotation = EditorGUILayout.TextField(this.mProtoFileAnotation, GUILayout.MaxWidth(200), GUILayout.MaxHeight(20));

        GUILayout.Label("字段个数", mGUIStyle, GUILayout.Width(60));
        this.mFieldCount = EditorGUILayout.IntField(this.mFieldCount, GUILayout.MaxWidth(30), GUILayout.MaxHeight(20));
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < mFieldCount; i++)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("字段类型", GUILayout.Width(60));
            mFieldTypeGroup[i] = EditorGUILayout.TextField(this.mFieldTypeGroup[i], GUILayout.MaxWidth(40), GUILayout.MaxHeight(20));
            GUILayout.Label("字段名", GUILayout.Width(60));
            mFieldNameGroup[i] = EditorGUILayout.TextField(this.mFieldNameGroup[i], GUILayout.MaxWidth(100), GUILayout.MaxHeight(20));
            GUILayout.Label("字段注释", GUILayout.Width(60));
            mFieldAnotationGroup[i] = EditorGUILayout.TextField(this.mFieldAnotationGroup[i], GUILayout.MaxWidth(100), GUILayout.MaxHeight(20));
            mIsLoopGroup[i] = EditorGUILayout.Toggle("是否循环", mIsLoopGroup[i]);


            EditorGUILayout.EndHorizontal();
            if (mIsLoopGroup[i])
            {
                Debug.Log("is loop!");
            }
        }




    }
}
