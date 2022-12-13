using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [Header("MasterVolume")]
    [SerializeField] SliderTextInput masterVolumeInput;
    [Header("SFXVolume")]
    [SerializeField] SliderTextInput sfxVolumeInput;
    [Header("MusicVolume")]
    [SerializeField] SliderTextInput musicVolumeInput;
    [Header("MouseSensitivity")]
    [SerializeField] SliderTextInput mouseSensitivityInput;


    // Start is called before the first frame update
    void Start()
    {

        float masterVolume = GetPrefFloatAndSetDefault("master_volume", 0.3f); 
        AudioManager.instance.ChangeMasterVolume(masterVolume);
        masterVolumeInput.SetValueWithoutNotify(masterVolume);

        //Begin SFX Volume
        float sfxVolume = GetPrefFloatAndSetDefault("sfx_volume", 0.3f);
        AudioManager.instance.ChangeSFXVolume(sfxVolume);
        sfxVolumeInput.SetValueWithoutNotify(sfxVolume);


        //Begin Music Volume
        float musicVolume = GetPrefFloatAndSetDefault("music_volume", 0.3f);
        AudioManager.instance.ChangeMusicVolume(musicVolume);
        musicVolumeInput.SetValueWithoutNotify(musicVolume);

        float mouseSensitivity = GetPrefFloatAndSetDefault("mouse_sensitivity", 7f);
        mouseSensitivityInput.SetValueWithoutNotify(mouseSensitivity);
        if (Player.Instance)
        {
            Player.Instance.mouseLook.mouseSensitivity = mouseSensitivity;
        }
    }

    private float GetPrefFloatAndSetDefault(string key, float defaultValue = 1f)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            Debug.LogAssertion("No key for: "+key);
            PlayerPrefs.SetFloat(key, defaultValue);
        }
        return PlayerPrefs.GetFloat(key);

    }

    public void SetMasterVolume()
    {
        AudioManager.instance.ChangeMasterVolume(masterVolumeInput.value); 
    }
    public void SFXVolume()
    {
        AudioManager.instance.ChangeSFXVolume(sfxVolumeInput.value);
    }
    public void MusicVolume()
    {
        AudioManager.instance.ChangeMusicVolume(musicVolumeInput.value);
    }
    public void MouseSensitivity()
    {
        PlayerPrefs.SetFloat("mouse_sensitivity", mouseSensitivityInput.value);
        if (Player.Instance)
        {
            Player.Instance.mouseLook.mouseSensitivity = mouseSensitivityInput.value;
        }
    }

}
