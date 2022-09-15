using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

public class SaverTest : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] private GameObject[] loadLevelButtons = new GameObject[14];
    [SerializeField] private GameObject firstLevel;
    [SerializeField] private GameObject TutorialText;
    [Header("Settings")]
    [SerializeField] private Slider soundVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;
    [SerializeField] private Toggle muteToggle;
    [SerializeField] private Slider sensetivitySlider;
    [SerializeField] private AudioMixerGroup _mixer;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoadData;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoadData;

    private void Start()
    {
        #if !UNITY_EDITOR
        if (YandexGame.SDKEnabled)
            GetLoadData();
        YandexGame.FullscreenShow();
                    
        #endif
        #if UNITY_EDITOR
                YandexGame.LoadLocal();
        #endif
    }

    public void Load() => YandexGame.LoadProgress();

    public void GetLoadData()
    {
        if (YandexGame.savesData.isFirstStart) return;
        
        if (YandexGame.savesData.isTutorailPassed)
        {
            firstLevel.GetComponent<Button>().interactable = true;
            TutorialText.SetActive(false);
        }
        Debug.Log(YandexGame.savesData.isTutorailPassed);
        for (int i = 0; i < loadLevelButtons.Length; i++)
            loadLevelButtons[i].GetComponent<Button>().interactable = YandexGame.savesData.openLevels[i];

        //загрузка настроек игры
        soundVolumeSlider.value = YandexGame.savesData.soundVolume;
        effectsVolumeSlider.value = YandexGame.savesData.effectVolume;


        muteToggle.isOn = YandexGame.savesData.isSoundActive;
        if (!muteToggle.isOn)
        {
            soundVolumeSlider.value = -80;
            effectsVolumeSlider.value = -80;
        }


        sensetivitySlider.value = YandexGame.savesData.sensetivity == 0 ? 0.4f : YandexGame.savesData.sensetivity;

        _mixer.audioMixer.SetFloat("MusicVolume", YandexGame.savesData.soundVolume);
        _mixer.audioMixer.SetFloat("EffectsVolume", YandexGame.savesData.effectVolume);
        
    }
}
