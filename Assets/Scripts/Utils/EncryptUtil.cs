//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-05-30 17:03:57
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------
using UnityEngine;
using System.Collections;
using System;
using System.IO;

/// <summary>
/// MD5加密
/// </summary>
public sealed class EncryptUtil
{
    public static string Md5(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return null;
        }
        System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] bytResult = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(value));
        string strResult = BitConverter.ToString(bytResult);
        strResult = strResult.Replace("-", "");
        return strResult;
    }

    /// <summary>
    /// 获取文件的MD5
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string GetFileMD5(string filePath)
    {
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
        {
            return null;
        }
        try
        {
            FileStream file = new FileStream(filePath, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytResult = md5.ComputeHash(file);
            string strResult = BitConverter.ToString(bytResult);
            strResult = strResult.Replace("-", "");
            return strResult;
        }
        catch
        {
            return null;
        }
    }
}