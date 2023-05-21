using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;


//����
public class MainManager : MonoBehaviour
{
    public TitleScreen TitleScreen;
    public UserBar UserBar;

    void Start()
    {
        GoogleLogin();
    }

    void GoogleLogin()
    {
        GPGSBinder.Inst.Login((success, localUser) => LoginSuccess(localUser));
    }

    void LoginSuccess(ILocalUser localuser)
    {
        TitleScreen.isPossibleScreenTouch = true; // Ÿ��Ʋȭ�� ��ġ���� -> ��ġ�ϸ� ����ȭ������
        UserBar.UserDataUpdate(localuser.id);
    }

}
