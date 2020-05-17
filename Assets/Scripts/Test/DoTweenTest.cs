using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Leo_DoTweenTest : MonoBehaviour {
    public Transform image;
    DOTweenAnimation animation;
    void Start()
    {
        //Test1();
        //Test2();
        //Test3();
        Test4();
    }

    void Update()
    {
        //Test1Main();
    }


    #region Test1


    private float test;

    void Test1()
    {

        DOTween.To(() => test, x => test = x, 200, 0.7f);
        
    }

    void Test1Main()
    {
        image.localPosition = new Vector3(test,image.localPosition.y);

    }
    #endregion

    #region Test2
    void Test2()
    {
        animation = image.GetComponent<DOTweenAnimation>();
        animation.DOPlay();
    }
    

    #endregion

    #region Test3
    void Test3()
    {
        Tweener tween= image.DOLocalMoveX(200, 0.7f);
        tween.OnComplete(OnComplete3);
    }
    void OnComplete3()
    {
        Debug.Log("1111");
    }

    #endregion

    #region Test4
    void Test4()
    {
        Sequence seq = DOTween.Sequence();
        //seq.Append(image.DOLocalMoveX(150, 0.7f));
        seq.Append(image.DOLocalMoveY(150, 0.7f));
        //seq.PrependInterval(2f);
        seq.Append(image.DOMove(new Vector3(150, 150), 0.7f));
        seq.OnComplete(OnComplete4);
    }
    void OnComplete4() {
        Debug.Log("444");
    }

    #endregion
}
