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
        // ���� ���� ������ �̹����� �´� ProfileImageNum ����
        //ProfileImageNum = 0; // ProfileImageNum�� ���Ŀ� ������ ���� �ʿ�

        // ���� ���� ������ �̹����� �´� �̹����� ȭ��ǥ�� ǥ�õ�
        MoveArrow(ProfileImageNum);
    }

    public void MoveArrow(int imagenum) // imagenum�� �ش��ϴ� �̹������� ȭ��ǥ�� �̵� ��Ű�� �Լ� + �̹������� onclick
    {
        Arrow.SetParent(Images[imagenum]);
        Arrow.localPosition = new Vector3 (0, 100, 0);
    }
}