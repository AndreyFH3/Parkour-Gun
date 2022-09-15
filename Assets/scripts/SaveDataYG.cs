using System.Collections;
using System.Collections.Generic;
using YG;
using UnityEngine;

public class SaveDataYG : MonoBehaviour
{
    [SerializeField] public bool tutorial;
    [SerializeField] public int levelPassed;
    
    public void SaveData()
    {
        YandexGame.savesData.isTutorailPassed = tutorial;
        Debug.Log(tutorial);
        YandexGame.savesData.isFirstStart = false;
        
        if (levelPassed <= YandexGame.savesData.maxPassedlevel) return ;
        YandexGame.savesData.maxPassedlevel = levelPassed;
        for (int i = 0; i < levelPassed; i++)
            YandexGame.savesData.openLevels[i] = true;
    }
}
