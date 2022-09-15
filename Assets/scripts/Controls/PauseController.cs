using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseObject;
    public static bool isPaused = false;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private AudioSource _audioSource;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (settingsMenu.activeSelf)
            {
                settingsMenu.SetActive(false);
                return;
            }
            SetPauseCondition();
        }
        //if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        //{
        //    Cursor.visible = true;
        //}
        //else if(Cursor.visible && Input.GetMouseButton(0) && !isPaused)
        //    Cursor.visible = false;
    }
    public void SetPauseCondition()
    {
        isPaused = !isPaused; // если тру, то фолс и наоборот
        pauseObject.SetActive(isPaused); // если тру, то показывает, если фолс вырубает 
        Time.timeScale = isPaused ? 0 : 1; // если тру, то время 0, если фолсе, то 1
        Cursor.visible = isPaused; // если тру, то видно, есил фолс, то невидно
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked; // если тру, то не залочен, если фолсе, то залочен
        if (isPaused) _audioSource.Stop();
    }
    public void ShowOrHideSettings()
    {
        if (!settingsMenu.activeSelf)
            settingsMenu.SetActive(true);
        else
            settingsMenu.SetActive(false);
    }
}
