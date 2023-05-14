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
        //È®ÀÎ ¹öÆ°À» ´©¸£¸é
        if (isPossibleName()) // ´Ğ³×ÀÓÀÌ ±ÔÄ¢¿¡ ¸Â´ÂÁö °Ë»ç
        {
            //ÇØ´ç ´Ğ³×ÀÓÀ¸·Î °áÁ¤ÇÒÁö ¹°¾îº½
            NoticeWindow.WindowSetting("ÇØ´ç ´Ğ³×ÀÓÀ¸·Î °áÁ¤ÇÏ½Ã°Ú½À´Ï±î?",false , SaveNickname);
        }
        else
        {
            //±ÔÄ¢¿¡ ¸ÂÁö ¾Ê´Â´Ù°í ¾Ë·ÁÁÜ
            NoticeWindow.WindowSetting(reason, true);
            reason = "";
        }
        NoticeWindow.gameObject.SetActive(true);
    }

    bool isPossibleName() 
    {
        string Name = InputField.text;

        if(Name == "")   //ºó ÅØ½ºÆ®ÀÎÁö È®ÀÎ
        {
            reason = "ÅØ½ºÆ®¸¦ ÀÔ·ÂÇÏÁö ¾Ê¾Ò½À´Ï´Ù. ´Ù½Ã ÀÔ·ÂÇÏ¿© ÁÖ¼¼¿ä.";
            return false;
        }
        else if(Name.Length > 8) // 8ÀÚ¸®¸¦ ³Ñ´ÂÁö È®ÀÎ
        {
            reason = "´Ğ³×ÀÓ ±æÀÌ°¡ 8ÀÚ¸®¸¦ ³Ñ¾ú½À´Ï´Ù. ´Ù½Ã ÀÏ·ÂÇÏ¿© ÁÖ¼¼¿ä.";
            return false;
        }
        else if(!Regex.IsMatch(Name, "^[0-9a-zA-Z°¡-ÆR]*$")) // ÇÑ±Û, ¿µ¾î, ¼ıÀÚ¸¸ ÀÔ·Â °¡´É
        {
            reason = "´Ğ³×ÀÓÀº ÇÑ±Û, ¿µ¾î, ¼ıÀÚ·Î¸¸ ÁöÀ» ¼ö ÀÖ½À´Ï´Ù.. ´Ù½Ã ÀÏ·ÂÇÏ¿© ÁÖ¼¼¿ä.";
            return false;
        }
        return true;
    }

    void SaveNickname()
    {
        nickname = InputField.text;
        //Å¬¶ó¿ìµå ÀúÀå
        GPGSBinder.Inst.SaveCloud("Nickname", nickname, success => SuccessSaveNickname());
    }

    void SuccessSaveNickname()
    {
        NoticeWindow.WindowSetting("´Ğ³×ÀÓ ÀúÀåÀÌ ¿Ï·áµÇ¾ú½À´Ï´Ù.", true, OffScreens);
        UserBar.UserDataUpdate(nickname);
    }

    void OffScreens()
    {
        TitleScreen.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
