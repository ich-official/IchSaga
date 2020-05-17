using UnityEngine;
using System.Collections;

public class Leo_AlgorithmTest : MonoBehaviour {

	// 1,1,2,3,5,8,13,21,34,55,89
	void Start () {
        int s=  FibnacciTest(1, 1, 10);
        Debug.Log(s);

	}
	
	// Update is called once per frame
	void Update () {
	
	}


    /**
     * 1、发现每次打印都是2，定位到问题是a经过递归只返回第一层值，补全【a=】FibnacciTest(a, a-b, x)问题解决
     * 2、FibnacciTest(a+b, a, x)打印的值不正确，发现递归时出现了(7,2,8)的参数，发现a+b为5+2，参数错误，根据f(n-2)+f(n-1)调整思想改为(a, a-b, x)，问题解决   
     */
    int FibnacciTest(int a,int b, int x)
    {
    //    if (x <= 0) return -1;
    //    if (x == 1 || x == 2) return 1;
        a = a + b;
        x--;       
        if(x>2)
        {
           a= FibnacciTest(a, a-b, x);            
        }
        return a;
    }
}
