using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//주현
public class TapButton : MonoBehaviour
{
    public Transform myTap;

    public void Onclick() //온클릭
    {
        myTap.SetAsLastSibling();
    }
}
