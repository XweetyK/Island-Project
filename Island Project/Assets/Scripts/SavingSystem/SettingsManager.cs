using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
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
            settings = new SettingsData(1,1);
        } else {settings= SaveSystem.LoadSettings(); }
    }

    private void Start() {
        UpdateSources();
    }
    private void Update() {
        if (_audio != null) {
            UpdateAudio();
        }
        Debug.Log(_audio);
    }

    public void UpdateSources() {
        _audio = null;
        _audio = FindObjectsOfType<AudioSource>();
    }

    void UpdateAudio() {
        if (_audio != null) {
            foreach (AudioSource clip in _audio) {
                if (clip.gameObject.tag == "MusicPlayer") {
                    clip.volume = settings.music;
                }
                if (clip.gameObject.tag == "SFXPlayer") {
                    clip.volume = settings.sfx;
                }
            }
        }
    }
}
