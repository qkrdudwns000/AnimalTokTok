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
    public TMP_Text NoticeText;
    public UserBar UserBar;

    public bool isPossibleScreenTouch = false;

    public void LoginSuccess()
    {
        NoticeText.text = "화면을 터치해 주세요!"; // Text내용 바꾸기
        isPossibleScreenTouch = true; //화면터치 가능
    }

    public void ScreenTouch() //OnClick
    {
        if (isPossibleScreenTouch) //로그인에 성공하였다면
        {
            this.gameObject.SetActive(false);
        }
    }
}
