using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NoticeWindow : MonoBehaviour
{
    public TMP_Text Contents;
    public Button SellectButton;

    public void WindowSetting(string contents, bool offwindow, UnityAction onclick = null)
    {
        //������ �۷� �����ֱ�
        Contents.text = contents;

        //��ư ��Ŭ�� ����
        SellectButton.onClick.RemoveAllListeners(); // ������ �ʱ�ȭ
        if(onclick != null) SellectButton.onClick.AddListener(onclick);
        if(offwindow) SellectButton.onClick.AddListener(UnActiveWindow);
    }

    void UnActiveWindow()
    {
        this.gameObject.SetActive(false);
    }
}
