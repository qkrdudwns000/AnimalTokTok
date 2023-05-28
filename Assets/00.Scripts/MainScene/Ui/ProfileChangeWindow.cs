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
        // ���� ���� ������ �̹����� �´� ProfileImageNum ����
        //ProfileImageNum = 0; // ProfileImageNum�� ���Ŀ� ������ ���� �ʿ�

        // ���� ���� ������ �̹����� �´� �̹����� ȭ��ǥ�� ǥ�õ�
        SelectImage(NowProfileImageNum);
    }

    public void SelectImage(int imagenum) // imagenum�� �ش��ϴ� �̹������� ȭ��ǥ�� �̵� ��Ű�� �Լ� + �̹������� onclick
    {
        SelectecdImage = Images[imagenum].GetChild(0).GetComponent<Image>().sprite; // ��������Ʈ ����
        NowProfileImageNum = imagenum; // �̹��� ��ȣ ����
        SaveInServer();

        Arrow.SetParent(Images[imagenum]);
        Arrow.localPosition = new Vector3 (0, 100, 0);
    }

    void SaveInServer()
    {
        // ���� ���� �� ���� �ʿ�
    }

    public void SaveProfile() // SaveButton Onclick
    {
        ProfileImage.sprite = SelectecdImage; // ������ �̹��� ����
        this.gameObject.SetActive(false); // â �ݱ�
    }
}
