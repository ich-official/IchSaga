using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
/// <summary>
/// 1、打开窗口，获取当前宏    2、点击按钮修改    3、保存完成修改
/// </summary>
public class PJSettingsWindow : EditorWindow {

    //宏ID，宏名字，测试服/正式服
    private List<SubItem> mList = new List<SubItem>();

    //宏名字，开/关
    private Dictionary<string, bool> mDic = new Dictionary<string, bool>();

    public string mDefineSymbol = "";

    public PJSettingsWindow()
    {
        //获取自定义宏的内容
        mDefineSymbol = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        mList.Clear();
        mDic.Clear();
        mList.Add(new SubItem() { name = "DEBUG_NOLOG",displayName="不打印测试log",isDebug=true,isRelease=false});
        mList.Add(new SubItem() { name = "DEBUG_LOG", displayName = "打印测试log", isDebug = true, isRelease = false });
        //正式服，有统计数据接入
        mList.Add(new SubItem() { name = "STAT_TD_NOADS", displayName = "正式服带统计无广告", isDebug = false, isRelease = true });
        mList.Add(new SubItem() { name = "STAT_NOTD_ADS", displayName = "正式服无统计带广告", isDebug = false, isRelease = true });
        mList.Add(new SubItem() { name = "STAT_TD_ADS", displayName = "正式服带统计带广告", isDebug = false, isRelease = true });
        //单机模式
        mList.Add(new SubItem() { name = "SINGLE_MODE", displayName = "单机模式", isDebug = true, isRelease = false });


        for (int i = 0; i < mList.Count; i++)
        {
            if (mDefineSymbol != null && mDefineSymbol.Contains(mList[i].name))
            {
                mDic[mList[i].name] = true;
            }
            else
            {
                mDic[mList[i].name] = false;
            }

        }
    }
    void OnGUI()
    {
        GUILayout.Label("hello world");
        for(int i=0; i<mDic.Count;i++)
        {
            EditorGUILayout.BeginHorizontal("box");
            //循环显示当前字典中所有宏定义项
            mDic[mList[i].name] = GUILayout.Toggle(mDic[mList[i].name], mList[i].displayName);
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("save", GUILayout.Width(100)))
        {
            mDefineSymbol = "";
            foreach (var item in mDic)
            {
                if (item.Value)
                {
                    mDefineSymbol += item.Key + ",";
                }
            }
            //最终保存到宏定义中
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, mDefineSymbol);

        }
    }


    public class SubItem
    {
        public string name;

        public string displayName;

        public bool isDebug;

        public bool isRelease;
    }
}
