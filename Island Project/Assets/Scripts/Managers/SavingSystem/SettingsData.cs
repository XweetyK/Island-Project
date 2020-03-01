using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public float music;
    public float sfx;
    public bool musicToggle;
    public bool sfxToggle;
    public int quality;

    public SettingsData (float musicVol, float sfxVol, bool musicEnabled, bool sfxEnabled, int qualityLvl) {
        music = musicVol;
        sfx = sfxVol;
        musicToggle = musicEnabled;
        sfxToggle = sfxEnabled;
        quality = qualityLvl;
    }
}
