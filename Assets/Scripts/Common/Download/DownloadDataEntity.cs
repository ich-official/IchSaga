//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-02 15:21:52
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 下载资源的实体类
/// </summary>
public class DownloadDataEntity  {
    /// <summary>
    /// 资源的文件名
    /// </summary>
    public string FullName;

    /// <summary>
    /// MD5
    /// </summary>
    public string MD5;

    /// <summary>
    /// 文件大小（K）
    /// </summary>
    public int Size;

    /// <summary>
    /// 是否初始数据
    /// </summary>
    public bool IsFirstData;

}
