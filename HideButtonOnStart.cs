using UnityEngine;

public class HideButtonOnStart : MonoBehaviour
{
    public GameObject buttonToHide; // —сылка на кнопку, которую нужно скрыть

    void Start()
    {
        if (buttonToHide != null)
        {
            buttonToHide.SetActive(false); // ќтключаем кнопку при запуске игры
        }
        else
        {
            Debug.LogWarning(" нопка дл€ скрыти€ не назначена!");
        }
    }
}