using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class EditorUtils  {

    [MenuItem("IchTools/Define Symbols")]
    public static void ProjectSettings()
    {
        UnityEngine.Debug.Log("hello orz");
        PJSettingsWindow window = (PJSettingsWindow)EditorWindow.GetWindow(typeof(PJSettingsWindow));
        window.titleContent = new GUIContent("宏定义工具");
        window.Show();
    
    }

    [MenuItem("IchTools/Build AB Assets")]
    public static void AssetBundleCreate()
    {
        //显示AB打包的窗口
        AssetBundleWindow win = EditorWindow.GetWindow<AssetBundleWindow>();
        win.titleContent = new GUIContent("AB打包工具");
        win.Show();
        
    }
}
