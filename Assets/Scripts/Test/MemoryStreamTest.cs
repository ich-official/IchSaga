using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Leo_MemoryStreamTest : MonoBehaviour {


	// Use this for initialization
	void Start () {

        ShopItemEntity entity = ShopItemDBModel.GetInstance().Get(1);
        if (entity != null)
        {
            Debug.Log(entity.Name);
        }
        RoleClassEntity entity1 = RoleClassDBModel.Instance.Get(1);
        Debug.Log(entity1.Name);
	}

    void GayLittleTest1()
    {
        Item item = new Item() { ID = 1, name = "leo" };
        byte[] ba = null;
        using (MemoryStreamUtil ms = new MemoryStreamUtil())
        {
            ms.WriteInt(item.ID);
            ms.WriteUTF8String(item.name);
            ba = ms.ToArray();
        }
        for (int i = 0; i < ba.Length; i++)
        {
            // Debug.Log(ba[i]);
        }
        Item item2 = new Item();
        using (MemoryStreamUtil ms = new MemoryStreamUtil(ba))
        {
            item.ID = ms.ReadInt();
            item.name = ms.ReadUTF8String();
        }
        Debug.Log(item.ID);
        Debug.Log(item.name);
    }

    void GayLittleTest2()
    {
        List<ShopItemEntity> list = ShopItemDBModel.GetInstance().GetAllData();
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log(list[i].Name);
        }
    }
}

public class Item
{
    public int ID;
    public string name;
}