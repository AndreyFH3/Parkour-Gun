using System.Collections;
using System.Collections.Generic;
using YG;
using UnityEngine;

public class ExitFromTutroial : MonoBehaviour
{
    private bool isPassed = false;
    [SerializeField] private GameObject WillLogin;
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.GetComponentInParent<movementControl>() && Input.GetKeyDown(KeyCode.F))
        {
            if (isPassed) return;
            //save data script
            GetComponent<SaveDataYG>().SaveData();
            YandexGame.SaveProgress();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            isPassed = true;
            WillLogin.SetActive(true);
        }
    }
}
