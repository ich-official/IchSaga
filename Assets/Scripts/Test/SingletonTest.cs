using UnityEngine;
using System.Collections;

public class Leo_SingletonTest  {
    private static Leo_SingletonTest single = null;
    public static Leo_SingletonTest GetInstance()
    {
        if (single == null)
        {
            single = new Leo_SingletonTest();
        }
        return single;
    }

}
