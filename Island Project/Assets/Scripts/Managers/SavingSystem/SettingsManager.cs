using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour {
    AudioSource[] _audio;
    public SettingsData settings;
    public static SettingsManager Instance { get; private set; }
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
        if (SaveSystem.LoadSettings() != null) {
            settings = new SettingsData(0.7f, 0.7f, true, true, 1);
        } else { settings = SaveSystem.LoadSettings(); }
    }

    private void Start() {
        UpdateSources();
    }
    private void Update() {
        if (_audio != null) {
            UpdateAudio();
        }
    }

    public void UpdateSources() {
        _audio = null;
        _audio = FindObjectsOfType<AudioSource>();
    }

    void UpdateAudio() {
        foreach (AudioSource clip in _audio) {
            if (clip != null && clip.gameObject.tag == "MusicPlayer") {
                clip.volume = settings.music;
            }
            if (clip != null && clip.gameObject.tag == "SFXPlayer" && clip != null) {
                clip.volume = settings.sfx;
            }
        }
    }
}
