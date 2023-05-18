using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


//ÁÖÇö
public class MainManager : MonoBehaviour
{
    public TitleScreen TitleScreen;
    public UserBar UserBar;

    void Start()
    {
        Debug.Log("MainManager.csÀÇ Start");
        GoogleLogin();
    }

    void GoogleLogin()
    {
        GPGSBinder.Inst.Login((success, localUser) => LoginSuccess());
        //string s = $"{success}, {localUser.userName}, {localUser.id}, {localUser.state}, {localUser.underage}");
    }

    void LoginSuccess()
    {
        TitleScreen.LoginSuccess();
    }

}
