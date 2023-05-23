using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;


//주현
public class MainManager : MonoBehaviour
{
    public TitleScreen TitleScreen;
    public UserBar UserBar;

    void Start()
    {
        //구글로그인
        //GoogleLogin();
    }

    void GoogleLogin()
    {
        GPGSBinder.Inst.Login((success, localUser) => LoginSuccess(localUser));
    }

    void LoginSuccess(ILocalUser localuser)
    {
        TitleScreen.LoginSuccess();
        UserBar.UserDataUpdate(localuser.id);
    }

}
