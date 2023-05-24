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

    bool isGoogleLoginSuccess()
    {
        // 추후 구글 연동 예정
        return true;
    }

    void Start()
    {
        if(isGoogleLoginSuccess())
        {
            LoginSuccess();
        }
    }

    void LoginSuccess()
    {
        TitleScreen.LoginSuccess();
        UserBar.UserDataUpdate("테스트 아이디");
    }

}
