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
        GoogleLogin();
    }

    void GoogleLogin()
    {
        GPGSBinder.Inst.Login((success, localUser) => LoginSuccess(localUser));
    }

    void LoginSuccess(ILocalUser localuser)
    {
        TitleScreen.isPossibleScreenTouch = true; // 타이틀화면 터치가능 -> 터치하면 메인화면으로
        UserBar.UserDataUpdate(localuser.id);
    }

}
