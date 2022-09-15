using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class logInScripts : MonoBehaviour
{
    public void switchAfterLogin() => LoadingLevel.SwitchToScene(0);
    public void logIn()
    {
        YandexGame.AuthDialog();    
    }
    private void Update()
    {
        if (gameObject.activeSelf && YandexGame.auth)
        {
            switchAfterLogin();
            gameObject.SetActive(false);
        }
    }
}
