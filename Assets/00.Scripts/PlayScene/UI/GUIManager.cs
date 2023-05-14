using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// (박영준) 게임플레이시 타이머 UI 관련 스크립트
public class GUIManager : MonoBehaviour
{
    public static GUIManager inst;
    private Animator myAnim = null;

    [SerializeField] TMPro.TMP_Text Txt_Score = null;
    [SerializeField] TMPro.TMP_Text Txt_Timer = null;
    [SerializeField] TMPro.TMP_Text Txt_AddTimer = null;

    private int score;

    public float maxTimer; // 최대 시간
    private float curTimer; // 현재 시간
    private float addTimer; // 추가 되는 시간
    private string AddTime = "AddTime";
    private bool isEnoughTime = true; // 현재 시간이 0초가 아닌지 확인하는 bool값
    public bool isStopWatch = false; // '스톱워치' 아이템이 사용되었는지 확인하는 bool값
    public float maxStopWatchTimer;
    private float curStopWatchTimer;

    private void Awake()
    {
        inst = this;
        myAnim = GetComponent<Animator>();
        score = 0; Txt_Score.text = score.ToString();
        curTimer = maxTimer;
        curStopWatchTimer = maxStopWatchTimer;
        isEnoughTime = true;
    }
    private void Update()
    {
        if(isEnoughTime && !isStopWatch)
            TimeAttack();
        if(isStopWatch)
            StopWatchItem();
    }

    public int Score
    {
        get { return score; }
        set 
        { 
            score = value; 
            Txt_Score.text = score.ToString(); 
        }
    }
    public float Timer
    {
        get { return curTimer; }
        set 
        { 
            curTimer = value; 
            if(curTimer >= maxTimer)
                curTimer = maxTimer;
        }
    }

    private void TimeAttack()
    {
        curTimer -= Time.deltaTime;
        Txt_Timer.text = curTimer.ToString("F2");

        if (curTimer <= 10.0f)
            Txt_Timer.color = Color.red;
        else
            Txt_Timer.color = Color.white;

        if(curTimer <= 0.0f)
        {
            curTimer = 0.0f;
            isEnoughTime = false;
        }
    }
    public void AddTimer(float _sec)
    {
        addTimer = _sec;
        Txt_AddTimer.text = "+" + addTimer.ToString() + " sec";
        myAnim.ResetTrigger(AddTime);
        myAnim.SetTrigger(AddTime);
    }
    private void StopWatchItem()
    {
        curStopWatchTimer -= Time.deltaTime;
        Txt_Timer.text = curTimer.ToString("F2");
        if (curStopWatchTimer <= 0.0f)
        {
            isStopWatch = false;
            curStopWatchTimer = maxStopWatchTimer;
        }
            
    }
}
