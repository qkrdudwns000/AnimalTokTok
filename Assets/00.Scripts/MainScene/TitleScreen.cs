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
    public GameObject MakeNicknameWindow;
    public UserBar UserBar;

    bool isPossibleScreenTouch = false;

    public void LoginSuccess()
    {
        //�ű����������Ǵ�
        GPGSBinder.Inst.SearchCloud("Nickname", OldUserAction, NewUserAction);
    }
    void NewUserAction()
    {
        Text.text = "�г��� ����� ��...";
        MakeNicknameWindow.SetActive(true);
    }

    void OldUserAction()
    {
        Text.text = "ȭ���� ��ġ�ϼ���.";
        isPossibleScreenTouch = true;
        //���������;�����Ʈ
        GPGSBinder.Inst.LoadCloud("Nickname", (success, data) => UserBar.UserDataUpdate(data));
    }

    public void ScreenTouch() //OnClick
    {
        if (isPossibleScreenTouch)//�α��ο� �����Ͽ��ٸ�
        {
            this.gameObject.SetActive(false);
        }
    }
}
