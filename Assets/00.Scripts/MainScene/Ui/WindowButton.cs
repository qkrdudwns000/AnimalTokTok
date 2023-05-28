using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowButton : MonoBehaviour
{
    public Windows Windows;
    GameObject MyWindow;

    public void OnClick(int winnum)
    {
        for (int i = 0; i < Windows.MyWindows.Length; i++)
        {
            if (i == winnum)
            {
                MyWindow = Windows.MyWindows[i];
                MyWindow.SetActive(!MyWindow.activeSelf);
            }
            else
            {
                Windows.MyWindows[i].SetActive(false);
            }
        }
    }
}
