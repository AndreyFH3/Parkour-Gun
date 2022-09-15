using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LoadingLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI LoadingPercent;
    [SerializeField] private Image LoadingProgressBar;

    private static LoadingLevel instance;
    private Animator _animator;
    private AsyncOperation loadSceneAsync;
    private static bool ShouldPlayAnimation = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        if (ShouldPlayAnimation)
        {
            _animator.SetTrigger("End");
            LoadingPercent.text = $"Loading...  100%";
            LoadingProgressBar.fillAmount = 1;
        }
        instance = this;
    }

    public static void SwitchToScene(int index)
    {
        Time.timeScale = 1;
        instance._animator.SetTrigger("Start");
        instance.loadSceneAsync =  SceneManager.LoadSceneAsync(index);
        instance.loadSceneAsync.allowSceneActivation = false;
        instance.StartCoroutine(nameof(loadingScene));
    }

    IEnumerator loadingScene()
    {
        while (!loadSceneAsync.isDone)
        {
            LoadingPercent.text = $"Loading...  {Mathf.RoundToInt(loadSceneAsync.progress / .9f * 100)}%";
            LoadingProgressBar.fillAmount = (loadSceneAsync.progress / 0.9f);
            yield return null;
        }
    }

    public void OnAnimationOver()
    {
        ShouldPlayAnimation = true;
        loadSceneAsync.allowSceneActivation = true;
    }
}
