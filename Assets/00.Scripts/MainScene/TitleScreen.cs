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

//����
public class TitleScreen : MonoBehaviour
{
    public TMP_Text Text;
    public UserBar UserBar;

    public bool isPossibleScreenTouch = false;

    public void LoginSuccess()
    {
        Text.text = "ȭ���� ��ġ�� �ּ���!"; // Text���� �ٲٱ�
        isPossibleScreenTouch = true; //ȭ����ġ ����
    }

    public void ScreenTouch() //OnClick
    {
        if (isPossibleScreenTouch) //�α��ο� �����Ͽ��ٸ�
        {
            this.gameObject.SetActive(false);
        }
    }
}
