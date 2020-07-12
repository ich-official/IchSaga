//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-05-11 12:24:55
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


namespace IchFramework
{
    /// <summary>
    /// 框架文件，启动游戏的唯一入口
    /// </summary>
    public class GameEntry : MonoBehaviour
    {
        #region 组件管理

        #region 组件声明
        public static EventComponent EventCompnt{ get; private set; }
        public static TimeComponent TimeCompnt{get; private set;}
        public static FsmComponent FsmCompnt{get; private set;}
        public static DataComponent DataCompnt{get; private set;}
        public static DataTableComponent DataTableCompnt{get; private set;}
        public static DownloadComponent DownloadCompnt{get; private set;}
        public static GameObjComponent GameObjCompnt{get; private set;}
        public static HttpComponent HttpCompnt{get; private set;}
        public static LocalizationComponent LocalizationCompnt{get; private set;}
        public static ObjPoolComponent ObjPoolCompnt{get; private set;}
        public static ProcedureComponent ProcedureCompnt{get; private set;}
        public static ResourceComponent ResourceCompnt{get; private set;}
        public static ScenesComponent SceneCompnt{get; private set;}
        public static SettingsComponent SettingsCompnt{get; private set;}
        public static SocketComponent SocketCompnt{get; private set;}
        public static UIComponent UICompnt{get; private set;}
        /// <summary>
        /// 存储基础组件的数据结构
        /// </summary>
        private static readonly LinkedList<IchComponentBase> mCompntList = new LinkedList<IchComponentBase>();

        #endregion

        #region 注册组件
        /// <summary>
        /// 注册组件，每种组件只允许注册一次
        /// </summary>
        /// <param name="component"></param>

        internal static void RegisterComponent(IchComponentBase component){
            //1.获取组件类型
            Type type=component.GetType();
            LinkedListNode<IchComponentBase> current=mCompntList.First;
            while(current!=null){
                if(current.Value.GetType()==type){
                    return;
                }
                current=current.Next;
            }
            mCompntList.AddLast(component); //把没有注册过的组件加入list中
          
        }

        #endregion

        #region 获取组件
        /// <summary>
        /// 获取自定义的组件
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static IchComponentBase GetIchComponent(Type type)
        {
            LinkedListNode<IchComponentBase> current = mCompntList.First;
            while (current != null)
            {
                if (current.Value.GetType() == type)
                {
                    return current.Value;
                }
                current = current.Next;
            }
            return null;
        }
        public static T GetIchComponent<T>() where T : IchComponentBase
        {
            return (T)GetIchComponent(typeof(T));
        }

        #endregion

        #region 给组件赋值
        private void InitIchComponent()
        {
            EventCompnt = GetIchComponent<EventComponent>();
            TimeCompnt = GetIchComponent<TimeComponent>();
            FsmCompnt = GetIchComponent<FsmComponent>();
            DataCompnt = GetIchComponent<DataComponent>();
            DataTableCompnt = GetIchComponent<DataTableComponent>();
            DownloadCompnt = GetIchComponent<DownloadComponent>();
            GameObjCompnt = GetIchComponent<GameObjComponent>();
            HttpCompnt = GetIchComponent<HttpComponent>();
            LocalizationCompnt = GetIchComponent<LocalizationComponent>();
            ObjPoolCompnt = GetIchComponent<ObjPoolComponent>();
            ProcedureCompnt = GetIchComponent<ProcedureComponent>();
            ResourceCompnt = GetIchComponent<ResourceComponent>();
            SceneCompnt = GetIchComponent<ScenesComponent>();
            SettingsCompnt = GetIchComponent<SettingsComponent>();
            SocketCompnt = GetIchComponent<SocketComponent>();
            UICompnt = GetIchComponent<UIComponent>();

        }
        #endregion

        #endregion

        #region 接口管理

        #region 接口声明

        /// <summary>
        /// 存储更新接口的数据结构
        /// </summary>
        private static readonly LinkedList<IUpdateComponent> mUpdateList = new LinkedList<IUpdateComponent>();

        #endregion

        #region 注册接口
        /// <summary>
        /// 注册更新接口
        /// </summary>
        /// <param name="component"></param>
        public static void RegisterUpdate(IUpdateComponent update)
        {
            mUpdateList.AddLast(update); //把update加入list中
        }

        #endregion

        #region 解除注册接口
        public static void RemoveUpdate(IUpdateComponent update)
        {
            //此处不用循环查找，链表的特性
            mUpdateList.Remove(update); //把update移除

        }

        #endregion
        #endregion
        void Start()
        {
            InitIchComponent();
            //PC的persist参考路径：C:/Users/Administrator/AppData/LocalLow/Ich_Official/IchSaga
            Debug.Log(Application.persistentDataPath);
        }

        void Update()
        {
            //循环一个链表的写法，把链表中所有的update执行一遍
            for (LinkedListNode<IUpdateComponent> current = mUpdateList.First; current != null; current = current.Next)
            {
                current.Value.OnUpdate();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                RemoveUpdate(UICompnt);
            }
        }

        void OnDestroy(){
            //1、关闭所有的基础组件
            for (LinkedListNode<IchComponentBase> current =  mCompntList.First; current != null; current = current.Next)
            {
                current.Value.Shutdown();
            }
        }
    }

}

