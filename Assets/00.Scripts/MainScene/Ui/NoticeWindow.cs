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
        //내용을 글로 보여주기
        Contents.text = contents;

        //버튼 온클릭 설정
        SellectButton.onClick.RemoveAllListeners(); // 리스너 초기화
        if(onclick != null) SellectButton.onClick.AddListener(onclick);
        if(offwindow) SellectButton.onClick.AddListener(UnActiveWindow);
    }

    void UnActiveWindow()
    {
        this.gameObject.SetActive(false);
    }
}
