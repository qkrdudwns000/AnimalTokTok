using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserBar : MonoBehaviour
{
    public TMP_Text Name;

    public void UserDataUpdate(string name)
    {
        Name.text = name;
    }
}
