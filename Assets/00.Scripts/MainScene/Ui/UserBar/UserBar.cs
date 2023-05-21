using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserBar : MonoBehaviour
{
    public TMP_Text ID;

    public void UserDataUpdate(string name)
    {
        ID.text = name;
    }
}
