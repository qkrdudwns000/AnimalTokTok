using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����
public class TapButton : MonoBehaviour
{
    public Transform myTap;

    public void Onclick() //��Ŭ��
    {
        myTap.SetAsLastSibling();
    }
}
