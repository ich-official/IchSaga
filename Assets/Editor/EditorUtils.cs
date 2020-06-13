using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

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

    [MenuItem("IchTools/Copy FirstData To StreamingAssets")]
    public static void AssetBundleCopyToStreamingAsstes()
    {
        //把初始资源复制到StreamingAssets文件夹内
        string toPath = Application.streamingAssetsPath + "/AssetBundles/";

        if (Directory.Exists(toPath))
        {
            Directory.Delete(toPath, true);
        }
        Directory.CreateDirectory(toPath);

        IOUtil.CopyDirectory(Application.persistentDataPath, toPath);
        AssetDatabase.Refresh();
        Debug.Log("拷贝完毕");
    }

}
