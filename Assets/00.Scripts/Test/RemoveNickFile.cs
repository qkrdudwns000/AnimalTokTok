using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveNickFile : MonoBehaviour
{
    public void RemoveButton()
    {
        GPGSBinder.Inst.DeleteCloud("Nickname", success => Debug.Log("������ �����Ǿ����"));
    }
}
