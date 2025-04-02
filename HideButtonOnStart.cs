using UnityEngine;

public class HideButtonOnStart : MonoBehaviour
{
    public GameObject buttonToHide; // ������ �� ������, ������� ����� ������

    void Start()
    {
        if (buttonToHide != null)
        {
            buttonToHide.SetActive(false); // ��������� ������ ��� ������� ����
        }
        else
        {
            Debug.LogWarning("������ ��� ������� �� ���������!");
        }
    }
}