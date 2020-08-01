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

    #region API���� ͨ��AccountId��gameServerId��ѯ��ɫ
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
    /// ���һ����ɫ
    /// </summary>
    /// <param name="classId">ְҵ����ID</param>
    /// <param name="nickName">�ǳ�</param>
    /// <param name="gameServerId">����ID</param>
    /// <returns></returns>
    public bool AddRole(int classId, string nickName,int gameServerId)
    {
        bool results;
        if (XMLHelper.Instance.Query_IsContain("Role.xml", "Role", "NickName", nickName))   //�ж��û����Ƿ��ظ�
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
    /// ɾ����ɫ������Ĵ����ݿ���ɾ�����ݣ����ǰѽ�ɫ��status״̬��Ϊ0��ɾ��״̬��
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public bool DeleteRole(int roleId)
    {
        bool results;
        //�ж��û����Ƿ��ظ�&&�ý�ɫ�Ƿ��Ѿ���ɾ��״̬
        if (XMLHelper.Instance.Query_IsContain("Role.xml", "Role", "Id", roleId.ToString()) //&&
           /* XMLHelper.Instance.Query_IsEquals("Role.xml", "Role", "Id", roleId.ToString())*/)   
        {
            Dictionary<string, string> conditions = new Dictionary<string, string>();   //�޸���������roleId=������roleId
            Dictionary<string, string> targets = new Dictionary<string, string>();      //�޸��ֶΣ�����ɫ״̬��Ϊ0��Deleted��
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
    /// ����roleId��ѯ�ý�ɫ����������
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
