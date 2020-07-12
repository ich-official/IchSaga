//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-17 17:45:04
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class ComponentsCollections : EditorWindow
{
    private Vector2 mVec2;
    private Vector3 mVec3;
    private Vector4 mVec4;
    private Color mColor;
    private float mSlider;
    private bool mIsSelectTog, mIsFoldout, mIsTogGroup;
    private bool[] mIsGroupTogs = new bool[2] { false, false };
    private Object mObj;
    private Texture mTexture;
    private string mSelectPath;
    private bool mTestIsSelectTog, mTestIsFoldout, mTestIsTogGroup;
    private string[] mTestIsGroupTogs = new string[50];

    private void OnEnable()
    {
        
    }

    public void OnGUI()
    {
        #region 坐标
        EditorGUILayout.BeginHorizontal();
        this.mVec2 = EditorGUILayout.Vector2Field("二维坐标", this.mVec2, GUILayout.MaxWidth(150));
        this.mVec3 = EditorGUILayout.Vector3Field("三维坐标", this.mVec3, GUILayout.MaxWidth(250));
        this.mVec4 = EditorGUILayout.Vector4Field("四维坐标", this.mVec4);
        EditorGUILayout.EndHorizontal();
        #endregion
        #region 颜色
        this.mColor = EditorGUILayout.ColorField(this.mColor);
        #endregion
        #region 滑动条（0-10）
        this.mSlider = EditorGUILayout.Slider(this.mSlider, 0, 10);
        #endregion
        #region 按钮和弹出框
        if (GUILayout.Button("点我"))
        {
            EditorUtility.DisplayDialog("标题", "提示内容", "确定", "取消");
            Debug.Log("path:" + mSelectPath);
        }
        #endregion
        #region 勾选、折叠
        this.mIsSelectTog = EditorGUILayout.Toggle("显示折叠", this.mIsSelectTog);
        if (this.mIsSelectTog)
        {
            this.mIsFoldout = EditorGUILayout.Foldout(this.mIsFoldout, "折叠");
            if (this.mIsFoldout)
            {
                this.mIsTogGroup = EditorGUILayout.BeginToggleGroup("选择：", this.mIsTogGroup);
                mIsGroupTogs[0] = EditorGUILayout.ToggleLeft("111", mIsGroupTogs[0]);
                mIsGroupTogs[1] = EditorGUILayout.ToggleLeft("222", mIsGroupTogs[1]);
                EditorGUILayout.EndToggleGroup();
            }
        }
        #endregion
        #region 对象选择
        //type-Object
        this.mObj = EditorGUILayout.ObjectField("选择object", this.mObj, typeof(Object));
        //type-Texture
        this.mTexture = (Texture)EditorGUILayout.ObjectField("选择图片", this.mTexture, typeof(Texture), true);

        #endregion
        #region 路径选择
        if (GUILayout.Button("选择", GUILayout.MaxWidth(80)))
        {
            this.mSelectPath = EditorUtility.OpenFilePanel("选择文件", "", "");
            Debug.Log("path:" + mSelectPath);
        }
        this.mSelectPath = EditorGUILayout.TextField(this.mSelectPath, GUILayout.MaxWidth(320), GUILayout.MaxHeight(20));
        #endregion



        
    }
}
public enum ShowType
{
    None = 0,
    IntType = 1,
    FloatType = 2,
    StrType = 3
}
