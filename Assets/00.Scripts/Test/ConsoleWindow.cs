using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsoleWindow : MonoBehaviour
{
    public static ConsoleWindow Inst;

    private void Awake()
    {
        Inst = this;
    }

    public void DebugLog(string text)
    {
        GetComponent<TMPro.TMP_Text>().text = text;
    }
}
