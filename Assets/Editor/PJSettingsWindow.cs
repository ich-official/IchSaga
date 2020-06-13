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

    public string mDefineSymbol = "";   //=m_Macor

    private void OnEnable()
    {
        //获取自定义宏的内容
        mDefineSymbol = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        mList.Clear();
        mDic.Clear();
        mList.Add(new SubItem() { name = "DEBUG_NOLOG", displayName = "不打印测试log", isDebug = true, isRelease = false });
        mList.Add(new SubItem() { name = "DEBUG_LOG", displayName = "打印测试log", isDebug = true, isRelease = false });
        //正式服，有统计数据接入
        mList.Add(new SubItem() { name = "STAT_TD_NOADS", displayName = "正式服带统计无广告", isDebug = false, isRelease = true });
        mList.Add(new SubItem() { name = "STAT_NOTD_ADS", displayName = "正式服无统计带广告", isDebug = false, isRelease = true });
        mList.Add(new SubItem() { name = "STAT_TD_ADS", displayName = "正式服带统计带广告", isDebug = false, isRelease = true });
        //单机模式
        mList.Add(new SubItem() { name = "SINGLE_MODE", displayName = "单机模式", isDebug = true, isRelease = false });
        //禁用AB资源的加载方式
        mList.Add(new SubItem() { name = "DISABLE_AB", displayName = "编辑器调试模式（禁用AB模式）", isDebug = true, isRelease = false });


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

    public PJSettingsWindow()
    {

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
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("save", GUILayout.Width(100)))
        {
            SaveDefineSymbol();
            /*
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
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, mDefineSymbol);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, mDefineSymbol);
            */
        }
        
        EditorGUILayout.EndHorizontal();
    }


    private void SaveDefineSymbol()
    {
        mDefineSymbol = string.Empty;
        foreach (var item in mDic)
        {
            if (item.Value)
            {
                mDefineSymbol += string.Format("{0};", item.Key);
            }
            //禁用AB加载时，把路径重设一下
            if (item.Key.Equals("DISABLE_AB", System.StringComparison.CurrentCultureIgnoreCase))
            {
                //打包界面的场景列表
                EditorBuildSettingsScene[] arrScene = EditorBuildSettings.scenes;
                for (int i = 0; i < arrScene.Length; i++)
                {
                    //如果场景列表里有“Download”，则把他的值设置为Value（本地测试时勾选，正式打包时去掉）
                    if (arrScene[i].path.IndexOf("Download", System.StringComparison.CurrentCultureIgnoreCase) > -1)
                    {
                        arrScene[i].enabled = item.Value;
                    }
                }
                //重新给场景列表赋值
                EditorBuildSettings.scenes = arrScene;
            }
        }
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, mDefineSymbol);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, mDefineSymbol);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, mDefineSymbol);
    }


    public class SubItem
    {
        public string name;

        public string displayName;

        public bool isDebug;

        public bool isRelease;
    }
}
