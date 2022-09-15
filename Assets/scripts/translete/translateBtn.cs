using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class translateBtn : MonoBehaviour
{
    private Button button;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { ChangeLanguage(); singleton.Instance.translateInvoke(); });
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    private void ChangeLanguage()
    {
        YG.YandexGame.savesData.language = YG.YandexGame.savesData.language == "ru" ? "en" : "ru";
    }
}
