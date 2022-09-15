using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class translate : MonoBehaviour
{
    [TextArea]
    [SerializeField] private string rus;
    [TextArea]
    [SerializeField] private string eng;
    private TextMeshProUGUI text;
    
    

    private void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        transalteVoid();
        singleton.Instance.translate += transalteVoid;
        YG.YandexGame.GetDataEvent += transalteVoid;
    }
    private void OnDisable()
    {
        singleton.Instance.translate -= transalteVoid;
        YG.YandexGame.GetDataEvent -= transalteVoid;
    }

    private void transalteVoid()
    {
        if (YG.YandexGame.savesData.language == "ru")
            text.text = rus;
        else
            text.text = eng;
    }
}
