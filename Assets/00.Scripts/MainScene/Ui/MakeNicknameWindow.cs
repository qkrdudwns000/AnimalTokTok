using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MakeNicknameWindow : MonoBehaviour
{
    public TMP_InputField InputField;
    public NoticeWindow NoticeWindow;
    public GameObject TitleScreen;
    public UserBar UserBar;

    string nickname = "";
    string reason = "";

    public void SellectButton() //onclick
    {
        //Ȯ�� ��ư�� ������
        if (isPossibleName()) // �г����� ��Ģ�� �´��� �˻�
        {
            //�ش� �г������� �������� ���
            NoticeWindow.WindowSetting("�ش� �г������� �����Ͻðڽ��ϱ�?",false , SaveNickname);
        }
        else
        {
            //��Ģ�� ���� �ʴ´ٰ� �˷���
            NoticeWindow.WindowSetting(reason, true);
            reason = "";
        }
        NoticeWindow.gameObject.SetActive(true);
    }

    bool isPossibleName() 
    {
        string Name = InputField.text;

        if(Name == "")   //�� �ؽ�Ʈ���� Ȯ��
        {
            reason = "�ؽ�Ʈ�� �Է����� �ʾҽ��ϴ�. �ٽ� �Է��Ͽ� �ּ���.";
            return false;
        }
        else if(Name.Length > 8) // 8�ڸ��� �Ѵ��� Ȯ��
        {
            reason = "�г��� ���̰� 8�ڸ��� �Ѿ����ϴ�. �ٽ� �Ϸ��Ͽ� �ּ���.";
            return false;
        }
        else if(!Regex.IsMatch(Name, "^[0-9a-zA-Z��-�R]*$")) // �ѱ�, ����, ���ڸ� �Է� ����
        {
            reason = "�г����� �ѱ�, ����, ���ڷθ� ���� �� �ֽ��ϴ�.. �ٽ� �Ϸ��Ͽ� �ּ���.";
            return false;
        }
        return true;
    }

    void SaveNickname()
    {
        nickname = InputField.text;
        //Ŭ���� ����
        GPGSBinder.Inst.SaveCloud("Nickname", nickname, success => SuccessSaveNickname());
    }

    void SuccessSaveNickname()
    {
        NoticeWindow.WindowSetting("�г��� ������ �Ϸ�Ǿ����ϴ�.", true, OffScreens);
        UserBar.UserDataUpdate(nickname);
    }

    void OffScreens()
    {
        TitleScreen.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
