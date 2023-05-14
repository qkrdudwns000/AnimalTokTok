using UnityEngine;

// (박영준) 블락 타입
public class Block : MonoBehaviour
{
    public bool blockType = true; // true = block, false = item
    public int colorType; // 0 = 원, 1 = 사각형, 2 = 원기둥, 3 = 피라미드, 4 = 다이아
}
