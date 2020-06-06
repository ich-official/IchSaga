using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;


public class AssetBundleDAL  {

    //XML路径
    private string mPath;

    //返回的资源实体集合
    private List<AssetBundleEntity> mList = null;

    public AssetBundleDAL(string path)
    {
        mPath = path;
        mList = new List<AssetBundleEntity>();
    }

    /// <summary>
    /// 读XML，把XML中配置的资源添加到list集合中
    /// </summary>
    /// <returns></returns>
    public List<AssetBundleEntity> GetList()
    {
        mList.Clear();
        //获取一个XML文件，参数为路径
        XDocument Xdoc = XDocument.Load(mPath);
        //获取这个XML的根节点
        XElement root = Xdoc.Root;
        //从根节点下找一个叫“AssetBundle”的节点
        XElement assetBundleNode = root.Element("AssetBundle");
        //从“AssetBundle”节点下找所有的“Item”节点，返回一个集合
        IEnumerable<XElement> itemList = assetBundleNode.Elements("Item");
        int itemKey=0;
        foreach (XElement item in itemList)
        {
            //创建实体，为每个属性赋值
            AssetBundleEntity entity = new AssetBundleEntity();
            entity.key = "LeoTestAB" + itemKey++;
            entity.Name = item.Attribute("Name").Value;
            entity.Tag = item.Attribute("Tag").Value;
            entity.IsFolder = item.Attribute("IsFolder").Value.Equals("true");
            entity.IsFirstData= item.Attribute("IsFirstData").Value.Equals("true");

            //item下的子标签path，也用集合的形式循环获取
            IEnumerable<XElement> pathList = item.Elements("Path");
            foreach (var path in pathList)
            {
                //Assets/后面是资源的路径，
                entity.pathList.Add(string.Format("{0}", path.Attribute("LocalPath").Value));
            }
            mList.Add(entity);
        }

        return mList;
    }
}
