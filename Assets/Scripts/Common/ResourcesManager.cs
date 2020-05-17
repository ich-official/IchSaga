//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-22 02:24:34
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------


using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

//继承了单例基类后，不用再重新写单例了，已经继承了父类的new单例的实例
public class ResourcesManager : SingletonBase<ResourcesManager>
{
    #region 资源类型枚举
    public enum ResourceType
    {
        ROLE,   //玩家主角
        EFFECT,  //特效
        UIOTHER,   //其他，目前包括模型头顶UI
        UIRoot,    //UI场景，总UI父节点
        UIPanel,   //UIPanel，挂载在container容器下
        UIItem,     //UIItem，各个panel下需要生成的小选项
        UI_SCENE,   //UI场景,NGUI使用，目前已弃用
        UI_WINDOW  //UI窗体,NGUI使用，目前已弃用
    }
    #endregion

    Dictionary<string, GameObject> mPrefabDic;

    public ResourcesManager()
    {
        mPrefabDic = new Dictionary<string, GameObject>();
    }


    /// <summary>
    /// 确认资源类型，加载资源，克隆到场景中（自带instance克隆）
    /// </summary>
    /// <param name="type">资源类型</param>
    /// <param name="path">短路径</param>
    /// <param name="isCache">是否缓存到dic中</param>
    /// <returns>资源的obj</returns>
    public GameObject Load(ResourceType type, string path,bool isCache=false)
    {
        
        GameObject obj = null;
        if (mPrefabDic.ContainsKey(path))
        {
            obj = mPrefabDic[path];
        }
        else
        {
            StringBuilder sb = new StringBuilder();
            switch (type)
            {
                case ResourceType.UIRoot:
                    sb.Append("UIPrefabs/UIScenes/UGUI/");
                    break;
                case ResourceType.UIPanel:
                    sb.Append("UIPrefabs/UIPanels/UGUI/");
                    break;
                case ResourceType.ROLE:
                    sb.Append("RolePrefabs/");
                    break;
                case ResourceType.EFFECT:
                    sb.Append("EffectPrefabs/");
                    break;
                case ResourceType.UIOTHER:
                    sb.Append("UIPrefabs/UIOthers/");
                    break;
                case ResourceType.UIItem:
                    sb.Append("UIPrefabs/UIItems/");
                    break;
                //以下是NGUI时的路径，目前已弃用
                case ResourceType.UI_SCENE:
                    sb.Append("UIPrefabs/UIScenes/NGUI/");
                    break;
                case ResourceType.UI_WINDOW:
                    sb.Append("UIPrefabs/UIWindows/NGUI/");
                    break;
                     
            }
            sb.Append(path);
            obj=Resources.Load(sb.ToString()) as GameObject;
            if (isCache)
            {
                mPrefabDic.Add(path, obj);
            }
        }
        
        /*此处造成了Setting the parent of a transform which resides in 
            a prefab is disabled to prevent data corruption.问题。
         * 原因是我返回了原始obj，而没有把Instantiate的克隆体返回，导致我
         * 设置transform都是对最原始的prefab去设置，就出现了上述问题。
         * 只要把return obj改为return GameObject.Instantiate(obj);就可解决问题
         */
        return GameObject.Instantiate(obj);
        //return obj;
    }

    public GameObject[] LoadScenes(string[] paths)
    {
        return null;
    }

    /// <summary>
    /// 切换场景时，把当前场景缓存的资源释放掉，节约内存
    /// </summary>
    public override void Dispose()
    {
        base.Dispose();
        mPrefabDic.Clear();
        Resources.UnloadUnusedAssets();
    }
}
