//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-08-04 01:14:44
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// �������½ǲ˵���view
/// </summary>
public class UIGizmosMainMenusView : MonoBehaviour {

    public static UIGizmosMainMenusView Instance;

    public Transform mMoveTarget; //����˵�ʱ���ƶ���Ŀ���
    [SerializeField]
    private Transform mPutInButton;
    private bool isMenuShow;
    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        isMenuShow = true;

        RegisterDoTweenAnims();
        
    }
	
    private void RegisterDoTweenAnims()
    {
        #region UI�ƶ�
        //�Դ���UI�������ϡ�����UIȫ���ƶ���Ŀ��λ��
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).DOLocalMove(mMoveTarget.localPosition, 0.2f)
            .SetAutoKill(false)
            .SetEase(GlobalInit.Instance.CommonAnimationCurve)
            .Pause()
            .OnComplete(() =>
            {

            })
            .OnRewind(() =>
            {

            });
        }
        #endregion

        #region UIScale
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).DOScale(0f, 0.2f)
            .SetAutoKill(false)
            .SetEase(GlobalInit.Instance.CommonAnimationCurve)
            .Pause()
            .OnComplete(() =>
            {

            })
            .OnRewind(() =>
            {

            });
        }
        #endregion

        #region PutInButton����
        mPutInButton.DOLocalRotate(new Vector3(0,0,-90),0.2f)
            .SetAutoKill(false)
            .SetEase(GlobalInit.Instance.CommonAnimationCurve)
            .Pause()
            .OnComplete(() =>
            {

            })
            .OnRewind(() =>
            {

            });
        #endregion
    }
    public void ChangeMenuStatus()
    {
        if (isMenuShow)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).DOPlayForward();
            }
            mPutInButton.DOPlayForward();
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).DOPlayBackwards();
            }
            mPutInButton.DOPlayBackwards();
        }
        isMenuShow = !isMenuShow;
    }
}
