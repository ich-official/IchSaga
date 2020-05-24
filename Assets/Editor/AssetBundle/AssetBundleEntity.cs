﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//AB实体类
public class AssetBundleEntity
{

    #region XML中item标签的属性
    //打包时给每个资源赋予一个key，确保唯一性
    public string key;

    public string Name;

    public string Tag;

    public string Version;

    public long Size;

    public string ToPath;
    #endregion

    #region XML中item下面子标签的属性集合
    private List<string> mPathList = new List<string>();

    public List<string> pathList
    {
        get { return mPathList; }
        
    }

    #endregion
}
