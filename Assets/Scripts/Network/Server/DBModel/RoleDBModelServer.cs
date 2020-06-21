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
/// 角色相关的DAO层。
/// </summary>
public class RoleDBModelServer : DBModelServerBase
{
    #region AccountDBModel 私有构造
    /// <summary>
    /// 私有构造
    /// </summary>
    private RoleDBModelServer()
    {
        //把数据库字段名和数据实体一一对应到字典里
    }
    #endregion

    #region 单例
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

    #region API方法 通过AccountId查询角色
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
