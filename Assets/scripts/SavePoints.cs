using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SavePoints : MonoBehaviour
{
    public  static SavePoints savePontsInstance;
    private static Vector3 savePoint;
    public  static bool isPointSet = false;
    [SerializeField] private GameObject cheater;
    private void Start()
    {
        if (savePontsInstance == null) savePontsInstance = this;
        if (isPointSet) transform.position = savePoint;
    }

    private void OnEnable()
    {
        YandexGame.CloseVideoEvent += SavePointVoid;
        YandexGame.CheaterVideoEvent += showCheaterAd;
    }

    private void OnDisable()
    {
        YandexGame.CloseVideoEvent -= SavePointVoid;
        YandexGame.CheaterVideoEvent -= showCheaterAd;
    }

    public void ShowAdReward(int levelindex) => YandexGame.RewVideoShow(levelindex);

    public void ExitLevel()
    {
        isPointSet = false;    
        LoadingLevel.SwitchToScene(0);
    }

    public void showCheaterAd() => cheater.SetActive(true);

    public void SavePointVoid(int id)
    {
        if (id == 1111)
        {
            savePoint = transform.position;
            isPointSet = true;
        }
    }
}
