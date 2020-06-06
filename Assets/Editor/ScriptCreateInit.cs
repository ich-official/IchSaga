//===================================================
//备    注：替换代码注释
//===================================================
using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEditor;

/// <summary>
/// 
/// </summary>
public class ScriptCreateInit : UnityEditor.AssetModificationProcessor 
{
    private static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta","");
        if (path.EndsWith(".cs"))
        {
            
            string strContent = File.ReadAllText(path);
            strContent = strContent.Replace("#AuthorName", "Ich")
                .Replace("#CreateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .Replace("#Version","1.0.0")
                .Replace("#ProjectURL", "https://github.com/ich-official/IchSaga")
                .Replace("#Email","ich_official@163.com")
                .Replace("OICQ_Group: #OICQ", "");

            File.WriteAllText(path, strContent);
            AssetDatabase.Refresh();
            
        }
    }
}