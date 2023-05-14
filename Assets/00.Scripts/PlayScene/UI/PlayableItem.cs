using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// (�ڿ���) �÷��̽� �����۹�ư UI ��ũ��Ʈ
public class PlayableItem : MonoBehaviour
{
    private Color selectColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);

    [SerializeField] GameObject go_RanibowSelect;
    [SerializeField] UnityEngine.UI.Image img_RainbowBomb;
    [SerializeField] UnityEngine.UI.Image img_StopWatch;

    bool isOpenSelectWindow = false;
    bool isUsedRainbowBomb = false; // 'RainbowBomb' �������� ����ߴ��� ���� bool��
    bool isUsedStopWatch = false; // 'StopWatch' �������� ����ߴ��� ���� bool��
 
    public void Btn_RainbowBomb()
    {
        if(!isOpenSelectWindow)
        {
            go_RanibowSelect.SetActive(true);
            isOpenSelectWindow = true;
        }
        else
        {
            go_RanibowSelect.SetActive(false);
            isOpenSelectWindow = false;
        }
    }
    public void Btn_SelectRainbow(int _tileNum)
    {
        if(!isUsedRainbowBomb)
        {
            PlayManager.inst.RainbowBombItem(_tileNum);
            img_RainbowBomb.color = selectColor;
            go_RanibowSelect.SetActive(false);
            isUsedRainbowBomb = true;
        }
    }
    public void Btn_StopWatch()
    {
        if(!isUsedStopWatch)
        {
            GUIManager.inst.isStopWatch = true;
            img_StopWatch.color = selectColor;
            isUsedStopWatch = true;
        }
    }
}
