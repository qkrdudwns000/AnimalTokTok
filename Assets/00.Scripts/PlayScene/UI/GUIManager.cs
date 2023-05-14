using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// (�ڿ���) �����÷��̽� Ÿ�̸� UI ���� ��ũ��Ʈ
public class GUIManager : MonoBehaviour
{
    public static GUIManager inst;
    private Animator myAnim = null;

    [SerializeField] TMPro.TMP_Text Txt_Score = null;
    [SerializeField] TMPro.TMP_Text Txt_Timer = null;
    [SerializeField] TMPro.TMP_Text Txt_AddTimer = null;

    private int score;

    public float maxTimer; // �ִ� �ð�
    private float curTimer; // ���� �ð�
    private float addTimer; // �߰� �Ǵ� �ð�
    private string AddTime = "AddTime";
    private bool isEnoughTime = true; // ���� �ð��� 0�ʰ� �ƴ��� Ȯ���ϴ� bool��
    public bool isStopWatch = false; // '�����ġ' �������� ���Ǿ����� Ȯ���ϴ� bool��
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
