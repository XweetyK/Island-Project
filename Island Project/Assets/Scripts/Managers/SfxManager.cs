using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFX { COMBAT, UI, PLAYER };
public class SfxManager : MonoBehaviour {
    [SerializeField] AudioClip[] _combat;
    [SerializeField] AudioClip[] _ui;
    [SerializeField] AudioClip[] _player;

    public static SfxManager Instance { get; private set; }
    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void Player(AudioSource source, SFX category, int clip, bool ovrd) {
        if (source == null) {
            source = this.gameObject.GetComponent<AudioSource>();
        }
        switch (category) {
            case SFX.COMBAT:
                if (clip > _combat.Length || clip < 0) { Debug.LogError("Clip number exceeds array"); } 
                else { Play(_combat[clip], source, ovrd); }
                break;
            case SFX.UI:
                if (clip > _ui.Length || clip < 0) { Debug.LogError("Clip number exceeds array"); } 
                else { Play(_ui[clip], source, ovrd); }
                break;
            case SFX.PLAYER:
                if (clip > _player.Length || clip < 0) { Debug.LogError("Clip number exceeds array"); } 
                else { Play(_player[clip], source, ovrd); }
                break;
        }
    }
    public void Stop(AudioSource source) {
        source.Stop();
    }
    private void Play(AudioClip clip, AudioSource source, bool ovrd) {
        source.clip = clip;
        if (ovrd) {
            source.Play();
        } else {
            if (!source.isPlaying) {
                source.Play();
            }
        }
    }
}
