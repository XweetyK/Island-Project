using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoader : MonoBehaviour
{
    [SerializeField] Slider _musicVol;
    [SerializeField] Slider _sfxVol;

    private void Start() {
        LoadSettings();
    }

    private void Update() {
        if (SettingsManager.Instance.settings != null) {
            if (_musicVol!=null || _sfxVol!=null) {
                SettingsManager.Instance.settings.music = _musicVol.value;
                SettingsManager.Instance.settings.sfx = _sfxVol.value;
            }
        } else { Debug.Log("null settings"); }
    }

    public void SaveSettings() {
        Debug.Log("settings saved");
        SaveSystem.SaveSettings(SettingsManager.Instance.settings);
    }
    public void LoadSettings() {
        Debug.Log("settings loaded");
        SettingsManager.Instance.settings = SaveSystem.LoadSettings();
        if (_musicVol != null || _sfxVol != null) {
            _musicVol.value = SettingsManager.Instance.settings.music;
            _sfxVol.value = SettingsManager.Instance.settings.sfx;
        }
    }
}
