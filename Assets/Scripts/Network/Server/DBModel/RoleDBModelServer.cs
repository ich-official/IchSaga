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

    #region API方法 通过AccountId和gameServerId查询角色
    public List<RoleEntity> GetRoles(int accountId,int gameServerId)
    {
        List<RoleEntity> list = new List<RoleEntity>();
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("AccountId", accountId.ToString());
        conditions.Add("GameServerId", gameServerId.ToString());
        List<Dictionary<string, string>> results=XMLHelper.Instance.Query("Role.xml", "Role", null, conditions);
        if (results.Count > 0)
        {
            for(int i = 0; i < results.Count; i++)
            {
                RoleEntity entity = new RoleEntity();
                entity.Id = int.Parse(results[i]["Id"]);
                entity.Status = (results[i]["Status"] == "0") ? EnumEntityStatus.Deleted : EnumEntityStatus.Released;
                entity.NickName = results[i]["NickName"];
                entity.ClassId = int.Parse(results[i]["ClassId"]);
                entity.Level = int.Parse(results[i]["Level"]);
                list.Add(entity);
            }
            

        }
        return list;
    }

    /// <summary>
    /// 添加一个角色
    /// </summary>
    /// <param name="classId">职业种类ID</param>
    /// <param name="nickName">昵称</param>
    /// <param name="gameServerId">区服ID</param>
    /// <returns></returns>
    public bool AddRole(int classId, string nickName,int gameServerId)
    {
        bool results;
        if (XMLHelper.Instance.Query_IsContain("Role.xml", "Role", "NickName", nickName))   //判断用户名是否重复
        {
            results = false;
        }
        else
        {
            Dictionary<string, string> targets = new Dictionary<string, string>();
            targets.Add("ClassId", classId.ToString());
            targets.Add("NickName", nickName.ToString());
            targets.Add("Status", EnumEntityStatus.Released.ToString());
            targets.Add("AccountId", GlobalCache.Instance.Account_CurrentId.ToString());
            targets.Add("GameServerId", gameServerId.ToString());
            results = XMLHelper.Instance.Add("Role.xml", "Role", targets);
        }

        return results;
        
    }
    /// <summary>
    /// 删除角色不是真的从数据库中删除数据，而是把角色的status状态改为0（删除状态）
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public bool DeleteRole(int roleId)
    {
        bool results;
        //判断用户名是否重复&&该角色是否已经是删除状态
        if (XMLHelper.Instance.Query_IsContain("Role.xml", "Role", "Id", roleId.ToString()) //&&
           /* XMLHelper.Instance.Query_IsEquals("Role.xml", "Role", "Id", roleId.ToString())*/)   
        {
            Dictionary<string, string> conditions = new Dictionary<string, string>();   //修改条件，即roleId=传来的roleId
            Dictionary<string, string> targets = new Dictionary<string, string>();      //修改字段，即角色状态改为0（Deleted）
            targets.Add("Status", "0");
            conditions.Add("Id", roleId.ToString());
            results = XMLHelper.Instance.Update("Role.xml", "Role", targets, conditions);
        }
        else
        {

            results = false;
        }
        return results;
    }

    /// <summary>
    /// 根据roleId查询该角色的属性详情
    /// </summary>
    /// <param name="roleId"></param>
    public Dictionary<string,string> GetRoleInfo(int roleId)
    {
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        Dictionary<string, string> result = new Dictionary<string, string>();
        conditions.Add("Id", roleId.ToString());
        result=XMLHelper.Instance.QueryOne("Role.xml", "Role", conditions);
        return result;
    }
    #endregion
}
