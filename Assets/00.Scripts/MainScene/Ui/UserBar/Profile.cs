using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile : MonoBehaviour
{
    public GameObject ProfileChangeWindow;

    public void OnClick()
    {
        //클릭하면 프로필변경 창이 뜨고 프로필을 변경할 수 있음
        ProfileChangeWindow.SetActive(true);
    }
}
