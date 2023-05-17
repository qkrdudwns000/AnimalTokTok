using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserBar : MonoBehaviour
{
    public TMP_Text Nickname;

    public void UserDataUpdate(string name)
    {
        Nickname.text = name;
    }
}
