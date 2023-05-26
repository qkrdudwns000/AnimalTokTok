using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileChangeWindow : MonoBehaviour
{
    public Image ProfileImage;
    public Transform Arrow;
    public Transform[] Images;

    int ProfileImageNum = 0;

    void OnEnable()
    {
        // 현재 나의 프로필 이미지에 맞는 ProfileImageNum 저장
        //ProfileImageNum = 0; // ProfileImageNum은 추후에 서버에 저장 필요

        // 현재 나의 프로필 이미지에 맞는 이미지에 화살표가 표시됨
        MoveArrow(ProfileImageNum);
    }

    public void MoveArrow(int imagenum) // imagenum에 해당하는 이미지위에 화살표를 이동 시키는 함수 + 이미지들의 onclick
    {
        Arrow.SetParent(Images[imagenum]);
        Arrow.localPosition = new Vector3 (0, 100, 0);
    }
}
