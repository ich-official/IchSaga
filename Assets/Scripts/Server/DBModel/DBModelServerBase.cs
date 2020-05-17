//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-29 02:13:39
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ģ������DBModel����
/// </summary>
public class DBModelServerBase  {


    #region ���߷���
    /// <summary>
    /// ����һ��errorMsg���ر����ݿ�
    /// </summary>
    public CallbackArgs GenerateErrorMsg(int errorCode=Constant.UNKNOWN,string errorMsg="unknown error!")
    {
        CallbackArgs args=new CallbackArgs();
        args.hasError = true;
        args.errorCode = errorCode;
        args.errorMsg = errorMsg;
        SqliteHelper.Instance.Disconnect();
        return args;
    }
    /// <summary>
    /// ����һ��sql������Ĵ�����Ϣ�������ܳ���
    /// </summary>
    /// <returns></returns>
    public CallbackArgs GenerateSqlErrorMsg()
    {
        return GenerateErrorMsg(Constant.SQL_ERROR, "sql��ѯ����");
    }
    /// <summary>
    /// ����һ����ȷ��Ϣ���ر����ݿ�
    /// </summary>
    public CallbackArgs GenerateSuccessMsg(List<ClientEntityBase> list=null,string json="empty string")
    {
        CallbackArgs args = new CallbackArgs();
        args.hasError = false;
        args.json = json;
        args.objList = list;
        SqliteHelper.Instance.Disconnect();
        return args;
    }

    public CallbackArgs GenerateSuccessMsg(ClientEntityBase entity = null, string json = "empty string")
    {
        CallbackArgs args = new CallbackArgs();
        args.hasError = false;
        args.json = json;
        args.obj = entity;
        SqliteHelper.Instance.Disconnect();
        return args;
    }
    #endregion 

}
