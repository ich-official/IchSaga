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
using System.Collections.Generic;
/// <summary>
/// 客户端本地DBModel的基类
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="P"></typeparam>
public abstract class AbstractDBModel<T,P> where T:new() where P:AbstractEntity {

    protected List<P> mList;
    protected Dictionary<int, P> mDic;

    #region 需要子类实现
    protected abstract string FileName   //文件名，不包括路径
    {
        get;
    }
    protected abstract P MakeEntity(GameDataTableParser parser);  //创建一个子类实体的对象

    #endregion
    /// <summary>
    /// 
    /// </summary>
    public AbstractDBModel()
    {
        mList = new List<P>();
        mDic = new Dictionary<int, P>();
        LoadData();
    }
    #region 单例
    private static T instance;

   
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
    public static T GetInstance()
    {
        if (instance == null)
        {
            instance = new T();
        }
        //
        return instance;
    }
    #endregion



    /// <summary>
    /// 读取，在创建对象时就把数据读取出来
    /// </summary>
    private void LoadData()
    {
        //参考streamingAsset路径：file:///data/app/com.ichgame.ichsaga-1/base.apk!/assets/AssetBundles/download\DataTable\Chapter.data
        //参考persist路径：/storage/emulated/0/Android/data/com.ichgame.ichsaga/files/download\DataTable\Chapter.data
        GameDataTableParser parser;
#if SINGLE_MODE && UNITY_EDITOR
        //TODO，这里的路径在以后热更新时做修改，现在是临时路径暂时写死
        parser = new GameDataTableParser(@"H:\IchSagaGit\IchSaga\Assets\StreamingAssets\AssetBundles\download\DataTable\" + FileName);
#elif SINGLE_MODE && UNITY_ANDROID
        parser = new GameDataTableParser(Application.persistentDataPath + @"/download\DataTable\" + FileName);
#endif
        while (!parser.Eof)
        {
            //由子类创建实体，返回来，即可知道P是哪个子类的对象
            P p = MakeEntity(parser);
            mList.Add(p);
            mDic[p.Id] = p;
            parser.Next();
        }
    }

    /// <summary>
    /// 获取子类里所有数据的对象
    /// </summary>
    /// <returns></returns>
    public List<P> GetAllData()
    {
        return mList;
    }
    /// <summary>
    /// 根据编号查询并返回对象
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public P Get(int ID)
    {
        if (mDic.ContainsKey(ID))
        {
            return mDic[ID];
        }
        else
        {
            return null;
        }

    }

}
