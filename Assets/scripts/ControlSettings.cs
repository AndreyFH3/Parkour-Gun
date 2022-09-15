using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using YG;

public class ControlSettings : MonoBehaviour
{
    [SerializeField] private Slider soundVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;
    [SerializeField] private Toggle muteToggle;

    [SerializeField] private AudioMixerGroup _mixer;

    [SerializeField] private Slider sensetivitySlider;
    public static float sensitivity = 0.4f;

    private void Start()
    {
        _mixer.audioMixer.GetFloat("MusicVolume", out float value);
        soundVolumeSlider.value = value;

        _mixer.audioMixer.GetFloat("EffectsVolume", out value);
        effectsVolumeSlider.value = value;
        sensetivitySlider.value = YandexGame.savesData.sensetivity;
    }
    //public void VolumeMasterChange(float value) 
    //{
    //    _mixer.audioMixer.SetFloat("MasterVolume", value);
    //    YandexGame.savesData.masterVolume = value;
    //    if (masterVolumeSlider.value == -80 && soundVolumeSlider.value == -80 && effectsVolumeSlider.value == -80)
    //        MuteAudio(false);
    //}

    private void OnDisable()
    {
        YandexGame.savesData.isFirstStart = false;
#if !UNITY_EDITOR
                YandexGame.SaveProgress();
#endif
#if UNITY_EDITOR
        YandexGame.SaveLocal();
#endif
    }
    public void VolumeMusicChange(float value) 
    { 
        _mixer.audioMixer.SetFloat("MusicVolume", value);
        YandexGame.savesData.soundVolume = value;
        if (soundVolumeSlider.value == -80 && effectsVolumeSlider.value == -80)
            muteToggle.isOn = false;
        else
            muteToggle.isOn = true;
    }

    public void VolumeEffectChange(float value)
    { 
        _mixer.audioMixer.SetFloat("EffectsVolume", value);
        YandexGame.savesData.effectVolume = value;
        if (soundVolumeSlider.value == -80 && effectsVolumeSlider.value == -80)
            muteToggle.isOn = false;
        else
            muteToggle.isOn = true;
    }

    public void MuteAudio(bool condition)
    {
        AudioListener.volume = condition ? 1 : 0;
        YandexGame.savesData.isSoundActive = condition;
        if (!condition)
        {
            soundVolumeSlider.value = -80;
            effectsVolumeSlider.value = -80;
        }
        else if(condition && soundVolumeSlider.value == -80 && effectsVolumeSlider.value == -80)
        {

            soundVolumeSlider.value = 0;
            effectsVolumeSlider.value = 0;
        }
    }

    public void sensetivitySettings(float value)
    {
        sensitivity = value;
        YandexGame.savesData.sensetivity = sensitivity;   
    }
}
