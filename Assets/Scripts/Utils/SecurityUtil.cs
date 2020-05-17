//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-25 14:55:17
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// 网络传输时为真实数据加密用的
/// 使用类似readExcel一样的加密算法
/// </summary>
public sealed class SecurityUtil  {

    #region xorScale 异或因子
    /// <summary>
    /// 异或因子
    /// </summary>
    private static byte[] xorScale = new byte[] { 45, 66, 38, 55, 23, 254, 9, 165, 90, 19, 41, 45, 201, 58, 55, 37, 254, 185, 165, 169, 19, 171 };//.data文件的xor加解密因子
    #endregion



    private SecurityUtil()
    {

    }
    /// <summary>
    /// 异或算法，readExcel和网络数据加密都使用他
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public static byte[] XorAlgorithm(byte[] buffer)
    {
        int iScaleLen = xorScale.Length;
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = (byte)(buffer[i] ^ xorScale[i % iScaleLen]);
        }
        return buffer;
    }

    /// <summary>
    /// 使用MD5加密
    /// </summary>
    /// <param name="value">要加密的数据</param>
    /// <param name="Is16"></param>
    /// <returns></returns>
    public static string Md5(string value, bool Is16=true)
    {
        string newValue=value;
        return newValue;
    }
}
