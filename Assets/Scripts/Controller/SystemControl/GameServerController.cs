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

/// <summary>
/// �������������շ������������Ϣ��
/// </summary>
public class GameServerController : ControllerBase<AccountController>, ISystemController
{
    public GameServerController()
    {
        AddEventListener("", null);
    }

    public void OpenView(UIWindowType windowType)
    {

    }
}
