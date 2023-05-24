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

    bool isGoogleLoginSuccess()
    {
        // ���� ���� ���� ����
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
        UserBar.UserDataUpdate("�׽�Ʈ ���̵�");
    }

}
