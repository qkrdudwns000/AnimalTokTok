using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileChangeWindow : MonoBehaviour
{
    public Image ProfileImage;
    public Transform Arrow;
    public Transform[] Images;

    public Sprite SelectecdImage;
    int NowProfileImageNum = 0;

    void OnEnable()
    {
        // 현재 나의 프로필 이미지에 맞는 ProfileImageNum 저장
        //ProfileImageNum = 0; // ProfileImageNum은 추후에 서버에 저장 필요

        // 현재 나의 프로필 이미지에 맞는 이미지에 화살표가 표시됨
        SelectImage(NowProfileImageNum);
    }

    public void SelectImage(int imagenum) // imagenum에 해당하는 이미지위에 화살표를 이동 시키는 함수 + 이미지들의 onclick
    {
        SelectecdImage = Images[imagenum].GetChild(0).GetComponent<Image>().sprite; // 스프라이트 저장
        NowProfileImageNum = imagenum; // 이미지 번호 저장
        SaveInServer();

        Arrow.SetParent(Images[imagenum]);
        Arrow.localPosition = new Vector3 (0, 100, 0);
    }

    void SaveInServer()
    {
        // 서버 연결 후 구현 필요
    }

    public void SaveProfile() // SaveButton Onclick
    {
        ProfileImage.sprite = SelectecdImage; // 프로필 이미지 변경
        this.gameObject.SetActive(false); // 창 닫기
    }
}
