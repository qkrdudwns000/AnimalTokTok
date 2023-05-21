using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SocialPlatforms;
using Unity.VisualScripting;
using System.Runtime.InteropServices.WindowsRuntime;

//주현
public class TitleScreen : MonoBehaviour
{
    public TMP_Text Text;
    public GameObject MakeNicknameWindow;
    public UserBar UserBar;

    public bool isPossibleScreenTouch = false;

    public void ScreenTouch() //OnClick
    {
        if (isPossibleScreenTouch)//로그인에 성공하였다면
        {
            this.gameObject.SetActive(false);
        }
    }
}
