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

    bool isPossibleScreenTouch = false;

    public void LoginSuccess()
    {
        //신규유저인지판단
        GPGSBinder.Inst.SearchCloud("Nickname", OldUserAction, NewUserAction);
    }
    void NewUserAction()
    {
        Text.text = "닉네임 만드는 중...";
        MakeNicknameWindow.SetActive(true);
    }

    void OldUserAction()
    {
        Text.text = "화면을 터치하세요.";
        isPossibleScreenTouch = true;
        //유저데이터업데이트
        //GPGSBinder.Inst.LoadCloud("Nickname", (success, data) => UserBar.UserDataUpdate(data));
    }

    public void ScreenTouch() //OnClick
    {
        if (isPossibleScreenTouch)//로그인에 성공하였다면
        {
            this.gameObject.SetActive(false);
        }
    }
}
