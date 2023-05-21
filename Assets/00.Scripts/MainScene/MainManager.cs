using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;


//ÁÖÇö
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
        TitleScreen.LoginSuccess();
        UserBar.UserDataUpdate(localuser.id);
    }

}
