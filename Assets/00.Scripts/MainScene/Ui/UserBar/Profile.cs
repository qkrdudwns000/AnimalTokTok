using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile : MonoBehaviour
{
    public GameObject ProfileChangeWindow;

    public void OnClick()
    {
        //Ŭ���ϸ� �����ʺ��� â�� �߰� �������� ������ �� ����
        ProfileChangeWindow.SetActive(true);
    }
}
