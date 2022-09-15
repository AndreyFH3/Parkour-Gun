using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dead : MonoBehaviour
{
    [SerializeField] private GameObject loseCanvas;
    [SerializeField] private AudioSource _audioSpurce;
    public static int deadCounter = 0;
    private static dead instanceDead;
    [SerializeField] GameObject activateAdReward;
    private void Start()
    {
        _audioSpurce = GetComponent<AudioSource>();
        if (instanceDead == null)
        {
            instanceDead = this;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<movementControl>())
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            loseCanvas.SetActive(true);
            _audioSpurce.Play();
            PauseController pc =  FindObjectOfType<PauseController>();
            pc.enabled = false;
            collision.transform.gameObject.SetActive(false);
            deadCounter++;
            if(deadCounter >= 5)
            {
                activateAdReward.SetActive(true);
                deadCounter = 0;
            }
        }
    }
}
