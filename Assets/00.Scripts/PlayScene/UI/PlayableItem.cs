using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// (박영준) 플레이시 아이템버튼 UI 스크립트
public class PlayableItem : MonoBehaviour
{
    private Color selectColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);

    [SerializeField] GameObject go_RanibowSelect;
    [SerializeField] UnityEngine.UI.Image img_RainbowBomb;
    [SerializeField] UnityEngine.UI.Image img_StopWatch;

    bool isOpenSelectWindow = false;
    bool isUsedRainbowBomb = false; // 'RainbowBomb' 아이템을 사용했는지 여부 bool값
    bool isUsedStopWatch = false; // 'StopWatch' 아이템을 사용했는지 여부 bool값
 
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
