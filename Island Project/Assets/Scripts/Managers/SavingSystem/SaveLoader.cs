using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveLoader : MonoBehaviour {
    [SerializeField] Slider _musicVol;
    [SerializeField] Slider _sfxVol;
    [SerializeField] Toggle _musicEnabled;
    [SerializeField] Toggle _sfxEnabled;
    [SerializeField] Dropdown _quality;

    public static SaveLoader Instance { get; private set; }
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

        private void Start() {
        LoadSettings();
    }

    private void Update() {
        if (SettingsManager.Instance.settings != null) {
            if (_musicVol != null || _sfxVol != null) {
                if (_musicEnabled.isOn) {
                    SettingsManager.Instance.settings.music = _musicVol.value;
                    SettingsManager.Instance.settings.musicToggle = true;
                } else {
                    SettingsManager.Instance.settings.music = 0.0f;
                    SettingsManager.Instance.settings.musicToggle = false;
                }

                if (_sfxEnabled.isOn) {
                    SettingsManager.Instance.settings.sfx = _sfxVol.value;
                    SettingsManager.Instance.settings.sfxToggle = true;
                } else {
                    SettingsManager.Instance.settings.sfx = 0.0f;
                    SettingsManager.Instance.settings.sfxToggle = false;
                }
            }

            if (_quality) {
                SettingsManager.Instance.settings.quality = _quality.value;
            }
        } else { Debug.Log("null settings"); }
        //if (Input.GetKeyDown(KeyCode.Y)) {
        //    SaveGame();
        //}
    }
    //SETTINGS------------------------------------------------------------------
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
            _musicEnabled.isOn = SettingsManager.Instance.settings.musicToggle;
            _sfxEnabled.isOn = SettingsManager.Instance.settings.sfxToggle;
        }
        if (_quality != null) {
            _quality.value = SettingsManager.Instance.settings.quality;
        }
    }

    public void SetQuality(Dropdown value) {
        switch (value.value) {
            case 0:
                QualitySettings.SetQualityLevel(1, true);
                break;
            case 1:
                QualitySettings.SetQualityLevel(3, true);
                break;
            case 2:
                QualitySettings.SetQualityLevel(5, true);
                break;
        }
    }
    //GAME-------------------------------------------------------------------
    public void SaveGame() {
        Debug.Log("Game saved");
        SaveGameManager.Instance.gameData = new SaveData(EventManager.Instance.Events, EventManager.Instance.GetMission, 
            GameObject.FindObjectOfType<PlayerMov>().transform,CharacterStats.Instance.GetStats(), SceneManager.GetActiveScene().name);
        SaveSystem.SaveGame(SaveGameManager.Instance.gameData);
    }
    public void LoadGame() {
        Debug.Log("Game loaded");
        if (SaveGameManager.Instance != null) {
            Debug.Log(SaveSystem.LoadGame()._key);
            SaveGameManager.Instance.gameData = SaveSystem.LoadGame();
            for (int i = 0; i < SaveGameManager.Instance.gameData._key.Length; i++) {
                EventManager.Instance.UpdateEvent(SaveGameManager.Instance.gameData._key[i], SaveGameManager.Instance.gameData._value[i]);
            }
        }
        EventManager.Instance.UpdateEvent("loadedGame", true);
        EventManager.Instance.UpdateMission(SaveGameManager.Instance.gameData._mission);
        SceneLoader.Instance.LoadScene(SaveGameManager.Instance.gameData._sceneName);
    }
}
