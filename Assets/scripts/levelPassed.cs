using System.Collections;
using System.Collections.Generic;
using YG;
using UnityEngine;

public class levelPassed : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject loseCanvas;
    [SerializeField] private AudioSource _audioSpurce;
    private void Start()
    {
        _audioSpurce = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<movementControl>())
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            loseCanvas.SetActive(true);
            _audioSpurce.Play();

            PauseController pc = FindObjectOfType<PauseController>();
            pc.enabled = false;
            collision.transform.gameObject.SetActive(false);

            dead.deadCounter = 0;
            //Тут добавить сохранения
            GetComponent<SaveDataYG>().SaveData();
           
            #if !UNITY_EDITOR
                YandexGame.SaveProgress();
            #endif
            #if UNITY_EDITOR
               YandexGame.SaveLocal();
#endif
            SavePoints.isPointSet = false;
        }

    }
}
