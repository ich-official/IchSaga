//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-21 14:29:48
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ɫ��ص�DAO�㡣
/// </summary>
public class RoleDBModelServer : DBModelServerBase
{
    #region AccountDBModel ˽�й���
    /// <summary>
    /// ˽�й���
    /// </summary>
    private RoleDBModelServer()
    {
        //�����ݿ��ֶ���������ʵ��һһ��Ӧ���ֵ���
    }
    #endregion

    #region ����
    private static object lock_object = new object();
    private static RoleDBModelServer instance = null;
    public static RoleDBModelServer Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lock_object)
                {
                    if (instance == null)
                    {
                        instance = new RoleDBModelServer();
                    }
                }
            }
            return instance;
        }
    }
    #endregion

    #region API���� ͨ��AccountId��ѯ��ɫ
    public List<RoleEntity> GetRoles(int accountId)
    {
        List<RoleEntity> list = new List<RoleEntity>();

        RoleEntity entity = new RoleEntity();
        entity.Id = 888;
        entity.NickName = "test";
        entity.ClassId = 1;
        entity.Level = 6;
        list.Add(entity);



        return list;
    }
    #endregion
}
