using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuScript : MonoBehaviour
{
    public GameObject settings;
    public void showSettings()
    {
        if(settings.activeSelf)
            settings.SetActive(false);
        else
            settings.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && settings.activeSelf)
            settings.SetActive(!settings.activeSelf);
    }
}
